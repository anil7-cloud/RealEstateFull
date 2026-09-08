using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationBatchRetryWorker
        : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<
            PropertyCustomerMatchSalesAutomationBatchRetryWorker> _logger;

        private static readonly TimeSpan CheckInterval =
            TimeSpan.FromMinutes(1);

        public PropertyCustomerMatchSalesAutomationBatchRetryWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<
                PropertyCustomerMatchSalesAutomationBatchRetryWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Batch retry worker started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessRetriesAsync(
                        stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Batch retry worker cycle failed.");
                }

                try
                {
                    await Task.Delay(
                        CheckInterval,
                        stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }

            _logger.LogInformation(
                "Batch retry worker stopped.");
        }

        private async Task ProcessRetriesAsync(
            CancellationToken cancellationToken)
        {
            using var scope =
                _scopeFactory.CreateScope();

            var repository =
                scope.ServiceProvider
                    .GetRequiredService<
                        PropertyCustomerMatchSalesAutomationBatchJobRepository>();

            var retryService =
                scope.ServiceProvider
                    .GetRequiredService<
                        PropertyCustomerMatchSalesAutomationBatchJobRetryService>();

            var metrics =
                scope.ServiceProvider
                    .GetRequiredService<
                        PropertyCustomerMatchSalesAutomationRetryMetricsService>();

            var eventRepository =
                scope.ServiceProvider
                    .GetRequiredService<
                        PropertyCustomerMatchSalesAutomationRetryEventRepository>();

            var failed =
                await repository.GetByStatusAsync(
                    "Failed",
                    500,
                    cancellationToken);

            var interrupted =
                await repository.GetByStatusAsync(
                    "Interrupted",
                    500,
                    cancellationToken);

            var now =
                DateTime.UtcNow;

            var candidates =
                failed
                    .Concat(interrupted)
                    .Where(x =>
                        x.RetryCount < 3 &&
                        x.NextRetryAt.HasValue &&
                        x.NextRetryAt.Value <= now)
                    .OrderBy(x =>
                        x.NextRetryAt)
                    .Take(25)
                    .ToList();

            if (candidates.Count == 0)
                return;

            _logger.LogInformation(
                "Found {Count} batch jobs ready for retry.",
                candidates.Count);

            foreach (var job in candidates)
            {
                cancellationToken
                    .ThrowIfCancellationRequested();

                Guid? claimToken =
                    null;

                try
                {
                    claimToken =
                        await repository
                            .TryClaimRetryAsync(
                                job.JobId,
                                TimeSpan.FromMinutes(10),
                                cancellationToken);

                    if (!claimToken.HasValue)
                    {
                        metrics.RecordClaimSkipped();

                        await WriteRetryEventAsync(
                            eventRepository,
                            job.JobId,
                            "ClaimSkipped",
                            "Skipped",
                            false,
                            job.RetryCount,
                            nextRetryAt: job.NextRetryAt,
                            cancellationToken: cancellationToken);

                        _logger.LogInformation(
                            "Automatic retry skipped. JobId={JobId} is already claimed.",
                            job.JobId);

                        continue;
                    }

                    await WriteRetryEventAsync(
                        eventRepository,
                        job.JobId,
                        "ClaimAcquired",
                        "Claimed",
                        true,
                        job.RetryCount,
                        claimToken: claimToken.Value,
                        cancellationToken: cancellationToken);

                    _logger.LogInformation(
                        "Retry claim acquired. JobId={JobId}, ClaimToken={ClaimToken}",
                        job.JobId,
                        claimToken.Value);

                    using var retryCts =
                        CancellationTokenSource.CreateLinkedTokenSource(
                            cancellationToken);

                    using var heartbeatCts =
                        CancellationTokenSource.CreateLinkedTokenSource(
                            cancellationToken);

                    var heartbeatTask =
                        RunClaimHeartbeatAsync(
                            job.JobId,
                            claimToken.Value,
                            retryCts,
                            heartbeatCts.Token);

                    try
                    {
                        metrics.RecordAttempt();

                        await WriteRetryEventAsync(
                            eventRepository,
                            job.JobId,
                            "RetryAttempted",
                            "Attempting",
                            true,
                            job.RetryCount,
                            claimToken: claimToken.Value,
                            cancellationToken: retryCts.Token);

                        var result =
                            await retryService
                                .RetryAsync(
                                    job.JobId,
                                    retryCts.Token);

                        if (result.Successful)
                        {
                            metrics.RecordSuccess();

                            await WriteRetryEventAsync(
                                eventRepository,
                                job.JobId,
                                "RetrySucceeded",
                                result.Status,
                                true,
                                job.RetryCount,
                                newJobId: result.NewJobId,
                                claimToken: claimToken.Value,
                                nextRetryAt: result.NextRetryAt,
                                cancellationToken: retryCts.Token);
                        }
                        else
                        {
                            switch (result.Status)
                            {
                                case "RetryBackoffActive":
                                    metrics.RecordBackoffRejected();
                                    break;

                                default:
                                    metrics.RecordFailure();

                                    await WriteRetryEventAsync(
                                        eventRepository,
                                        job.JobId,
                                        "RetryFailed",
                                        result.Status,
                                        false,
                                        job.RetryCount,
                                        newJobId: result.NewJobId,
                                        claimToken: claimToken.Value,
                                        nextRetryAt: result.NextRetryAt,
                                        errorMessage: result.ErrorMessage,
                                        cancellationToken: retryCts.Token);

                                    break;
                            }
                        }

                        _logger.LogInformation(
                            "Automatic retry JobId={JobId}, Status={Status}, NewJobId={NewJobId}",
                            job.JobId,
                            result.Status,
                            result.NewJobId);
                    }
                    finally
                    {
                        heartbeatCts.Cancel();

                        try
                        {
                            await heartbeatTask;
                        }
                        catch (OperationCanceledException)
                        {
                            // Expected when heartbeat is stopped.
                        }
                    }
                }
                catch (OperationCanceledException)
                    when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Automatic retry failed for JobId={JobId}",
                        job.JobId);
                }
                finally
                {
                    if (claimToken.HasValue)
                    {
                        try
                        {
                            var released =
                                await repository
                                    .ReleaseRetryClaimAsync(
                                        job.JobId,
                                        claimToken.Value,
                                        CancellationToken.None);

                            if (!released)
                            {
                                _logger.LogWarning(
                                    "Retry claim could not be released. JobId={JobId}",
                                    job.JobId);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(
                                ex,
                                "Retry claim release failed. JobId={JobId}",
                                job.JobId);
                        }
                    }
                }
            }
        }

        private async Task RunClaimHeartbeatAsync(
            Guid jobId,
            Guid claimToken,
            CancellationTokenSource retryCts,
            CancellationToken cancellationToken)
        {
            var heartbeatInterval =
                TimeSpan.FromMinutes(2);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(
                        heartbeatInterval,
                        cancellationToken);

                    using var heartbeatScope =
                        _scopeFactory.CreateScope();

                    var heartbeatRepository =
                        heartbeatScope.ServiceProvider
                            .GetRequiredService<
                                PropertyCustomerMatchSalesAutomationBatchJobRepository>();

                    var renewed =
                        await heartbeatRepository
                            .RenewRetryClaimAsync(
                                jobId,
                                claimToken,
                                cancellationToken);

                    if (!renewed)
                    {
                        var metrics =
                            heartbeatScope.ServiceProvider
                                .GetRequiredService<
                                    PropertyCustomerMatchSalesAutomationRetryMetricsService>();

                        metrics.RecordLeaseLost();

                        _logger.LogWarning(
                            "Retry claim heartbeat lost. JobId={JobId}, ClaimToken={ClaimToken}. Retry will be cancelled.",
                            jobId,
                            claimToken);

                        if (!retryCts.IsCancellationRequested)
                        {
                            retryCts.Cancel();
                        }

                        return;
                    }

                    _logger.LogDebug(
                        "Retry claim heartbeat renewed. JobId={JobId}",
                        jobId);
                }
                catch (OperationCanceledException)
                    when (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Retry claim heartbeat failed. JobId={JobId}",
                        jobId);

                    return;
                }
            }
        }


        private async Task WriteRetryEventAsync(
            PropertyCustomerMatchSalesAutomationRetryEventRepository repository,
            Guid jobId,
            string eventType,
            string status,
            bool successful,
            int retryCount,
            Guid? newJobId = null,
            Guid? claimToken = null,
            DateTime? nextRetryAt = null,
            string? errorMessage = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var entity =
                    new REAL_ESTATE_CLEAN.Core.Domain.Entities
                        .PropertyMatchSalesAutomationRetryEvent
                    {
                        Id =
                            Guid.NewGuid(),

                        JobId =
                            jobId,

                        NewJobId =
                            newJobId,

                        ClaimToken =
                            claimToken,

                        RetryCount =
                            retryCount,

                        EventType =
                            eventType,

                        Status =
                            status,

                        Successful =
                            successful,

                        ErrorMessage =
                            errorMessage,

                        NextRetryAt =
                            nextRetryAt,

                        CreatedAt =
                            DateTime.UtcNow
                    };

                await repository.AddAsync(
                    entity,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Retry event persistence failed. JobId={JobId}, EventType={EventType}",
                    jobId,
                    eventType);
            }
        }

    }
}
