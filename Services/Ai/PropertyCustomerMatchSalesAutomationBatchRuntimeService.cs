using System.Collections.Concurrent;
using System.Diagnostics;
using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationBatchRuntimeService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationRuntimeService
                _runtimeService;

        public PropertyCustomerMatchSalesAutomationBatchRuntimeService(
            PropertyCustomerMatchSalesAutomationRuntimeService runtimeService)
        {
            _runtimeService = runtimeService;
        }

        public async Task<PropertyMatchSalesAutomationBatchRuntimeResultDto>
            RunAsync(
                PropertyMatchSalesAutomationBatchRuntimeRequestDto request,
                CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            var matchIds =
                request.MatchIds
                    .Where(x => x > 0)
                    .Distinct()
                    .Take(NormalizeBatchSize(request.MaxBatchSize))
                    .ToList();

            if (matchIds.Count == 0)
            {
                throw new ArgumentException(
                    "En az bir geçerli MatchId gereklidir.",
                    nameof(request));
            }

            var stopwatch =
                Stopwatch.StartNew();

            var result =
                new PropertyMatchSalesAutomationBatchRuntimeResultDto
                {
                    BatchId = Guid.NewGuid(),
                    Status = "Running",
                    TotalRequested = matchIds.Count,
                    StartedAt = DateTime.UtcNow
                };

            var results =
                new ConcurrentBag<
                    PropertyCustomerMatchSalesAutomationRuntimeDto>();

            var errors =
                new ConcurrentBag<
                    PropertyMatchSalesAutomationBatchErrorDto>();

            var concurrency =
                NormalizeConcurrency(
                    request.MaxConcurrency);

            using var semaphore =
                new SemaphoreSlim(
                    concurrency,
                    concurrency);

            var tasks =
                matchIds.Select(
                    async matchId =>
                    {
                        await semaphore.WaitAsync(
                            cancellationToken);

                        try
                        {
                            cancellationToken
                                .ThrowIfCancellationRequested();

                            var runtimeRequest =
                                new PropertyCustomerMatchSalesAutomationRuntimeRequestDto
                                {
                                    MatchId =
                                        matchId,

                                    Actor =
                                        request.Actor,

                                    RequireApproval =
                                        request.RequireApproval,

                                    ExecuteImmediately =
                                        request.ExecuteImmediately,

                                    PersistAudit =
                                        request.PersistAudit,

                                    CorrelationId =
                                        $"{request.CorrelationId}:{matchId}"
                                };

                            var runtimeResult =
                                await _runtimeService
                                    .RunAsync(runtimeRequest);

                            results.Add(
                                runtimeResult);
                        }
                        catch (OperationCanceledException)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            errors.Add(
                                new PropertyMatchSalesAutomationBatchErrorDto
                                {
                                    MatchId =
                                        matchId,

                                    ErrorCode =
                                        ex.GetType().Name,

                                    ErrorMessage =
                                        ex.Message,

                                    OccurredAt =
                                        DateTime.UtcNow
                                });
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    })
                .ToList();

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (OperationCanceledException)
            {
                result.Status =
                    "Cancelled";
            }

            stopwatch.Stop();

            result.Results =
                results
                    .OrderBy(x => x.MatchId)
                    .ToList();

            result.Errors =
                errors
                    .OrderBy(x => x.MatchId)
                    .ToList();

            result.Completed =
                result.Results.Count(
                    x =>
                        x.Status == "Completed");

            result.AwaitingApproval =
                result.Results.Count(
                    x =>
                        x.Status == "AwaitingApproval");

            result.Failed =
                result.Results.Count(
                    x =>
                        !x.Successful &&
                        x.Status != "AwaitingApproval")
                +
                result.Errors.Count;

            result.Successful =
                result.Results.Count(
                    x => x.Successful);

            result.Processed =
                result.Results.Count +
                result.Errors.Count;

            result.SuccessRate =
                Percentage(
                    result.Successful,
                    result.Processed);

            result.FailureRate =
                Percentage(
                    result.Failed,
                    result.Processed);

            result.AverageDurationMilliseconds =
                result.Results.Count == 0
                    ? 0
                    : Math.Round(
                        result.Results.Average(
                            x =>
                                (decimal)
                                x.DurationMilliseconds),
                        2);

            if (result.Status != "Cancelled")
            {
                result.Status =
                    result.Failed == 0
                        ? "Completed"
                        : result.Successful > 0 ||
                          result.AwaitingApproval > 0
                            ? "PartiallyCompleted"
                            : "Failed";
            }

            result.CompletedAt =
                DateTime.UtcNow;

            result.DurationMilliseconds =
                stopwatch.ElapsedMilliseconds;

            return result;
        }

        private static int NormalizeConcurrency(
            int value)
        {
            if (value < 1)
                return 1;

            return Math.Min(
                value,
                20);
        }

        private static int NormalizeBatchSize(
            int value)
        {
            if (value < 1)
                return 100;

            return Math.Min(
                value,
                1000);
        }

        private static decimal Percentage(
            int value,
            int total)
        {
            if (total <= 0)
                return 0;

            return Math.Round(
                ((decimal)value / total) * 100m,
                2);
        }
    }

    public class PropertyMatchSalesAutomationBatchRuntimeRequestDto
    {
        public List<int> MatchIds { get; set; }
            = new();

        public string Actor { get; set; }
            = "System";

        public bool RequireApproval { get; set; }

        public bool ExecuteImmediately { get; set; }
            = true;

        public bool PersistAudit { get; set; }
            = true;

        public int MaxConcurrency { get; set; }
            = 5;

        public int MaxBatchSize { get; set; }
            = 100;

        public string CorrelationId { get; set; }
            = Guid.NewGuid().ToString("N");
    }

    public class PropertyMatchSalesAutomationBatchRuntimeResultDto
    {
        public Guid BatchId { get; set; }

        public string Status { get; set; }
            = string.Empty;

        public int TotalRequested { get; set; }

        public int Processed { get; set; }

        public int Successful { get; set; }

        public int Completed { get; set; }

        public int AwaitingApproval { get; set; }

        public int Failed { get; set; }

        public decimal SuccessRate { get; set; }

        public decimal FailureRate { get; set; }

        public decimal AverageDurationMilliseconds { get; set; }

        public long DurationMilliseconds { get; set; }

        public List<
            PropertyCustomerMatchSalesAutomationRuntimeDto>
            Results { get; set; } = new();

        public List<
            PropertyMatchSalesAutomationBatchErrorDto>
            Errors { get; set; } = new();

        public DateTime StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationBatchErrorDto
    {
        public int MatchId { get; set; }

        public string ErrorCode { get; set; }
            = string.Empty;

        public string ErrorMessage { get; set; }
            = string.Empty;

        public DateTime OccurredAt { get; set; }
    }
}
