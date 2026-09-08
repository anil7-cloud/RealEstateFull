using System.Collections.Concurrent;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationBatchJobService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationBatchRuntimeService
                _batchRuntimeService;

        private readonly ConcurrentDictionary<
            Guid,
            PropertyMatchSalesAutomationBatchJobDto>
                _jobs = new();

        private readonly
            PropertyCustomerMatchSalesAutomationBatchJobRepository
                _repository;

        public PropertyCustomerMatchSalesAutomationBatchJobService(
            PropertyCustomerMatchSalesAutomationBatchRuntimeService batchRuntimeService,
            PropertyCustomerMatchSalesAutomationBatchJobRepository repository)
        {
            _batchRuntimeService =
                batchRuntimeService;

            _repository =
                repository;
        }

        public async Task<PropertyMatchSalesAutomationBatchJobDto>
            CreateAndRunAsync(
                PropertyMatchSalesAutomationBatchRuntimeRequestDto request,
                CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            var job =
                new PropertyMatchSalesAutomationBatchJobDto
                {
                    JobId =
                        Guid.NewGuid(),

                    Status =
                        "Running",

                    Actor =
                        request.Actor,

                    TotalRequested =
                        request.MatchIds?
                            .Where(x => x > 0)
                            .Distinct()
                            .Count()
                        ?? 0,

                    CreatedAt =
                        DateTime.UtcNow,

                    StartedAt =
                        DateTime.UtcNow
                };

            _jobs[job.JobId] =
                job;

            await CreatePersistentJobAsync(
                job,
                request,
                cancellationToken);

            try
            {
                var result =
                    await _batchRuntimeService
                        .RunAsync(
                            request,
                            cancellationToken);

                job.BatchId =
                    result.BatchId;

                job.Status =
                    result.Status;

                job.Processed =
                    result.Processed;

                job.Successful =
                    result.Successful;

                job.Completed =
                    result.Completed;

                job.Failed =
                    result.Failed;

                job.AwaitingApproval =
                    result.AwaitingApproval;

                job.SuccessRate =
                    result.SuccessRate;

                job.FailureRate =
                    result.FailureRate;

                job.DurationMilliseconds =
                    result.DurationMilliseconds;

                job.Result =
                    result;

                job.CompletedAt =
                    DateTime.UtcNow;

                job.UpdatedAt =
                    DateTime.UtcNow;

                await UpdatePersistentJobAsync(
                    job,
                    cancellationToken);

                return job;
            }
            catch (OperationCanceledException)
            {
                job.Status =
                    "Cancelled";

                job.ErrorMessage =
                    "Batch job iptal edildi.";

                job.CompletedAt =
                    DateTime.UtcNow;

                job.UpdatedAt =
                    DateTime.UtcNow;

                await UpdatePersistentJobSafeAsync(
                    job);

                throw;
            }
            catch (Exception ex)
            {
                job.Status =
                    "Failed";

                job.ErrorCode =
                    ex.GetType().Name;

                job.ErrorMessage =
                    ex.Message;

                job.CompletedAt =
                    DateTime.UtcNow;

                job.UpdatedAt =
                    DateTime.UtcNow;

                await UpdatePersistentJobSafeAsync(
                    job);

                return job;
            }
        }

        public async Task<PropertyMatchSalesAutomationBatchJobDto?>
            GetAsync(
                Guid jobId)
        {
            if (_jobs.TryGetValue(
                jobId,
                out var memoryJob))
            {
                return memoryJob;
            }

            var entity =
                await _repository
                    .GetByJobIdAsync(
                        jobId);

            if (entity == null)
                return null;

            var job =
                MapPersistentEntityToDto(
                    entity);

            _jobs[job.JobId] =
                job;

            return job;
        }

        public async Task<List<PropertyMatchSalesAutomationBatchJobDto>>
            GetAllAsync(
                int limit = 100)
        {
            var entities =
                await _repository
                    .GetAllAsync(
                        NormalizeLimit(limit));

            var jobs =
                entities
                    .Select(
                        MapPersistentEntityToDto)
                    .ToList();

            foreach (var job in jobs)
            {
                _jobs[job.JobId] =
                    job;
            }

            return jobs;
        }

        public async Task<List<PropertyMatchSalesAutomationBatchJobDto>>
            GetRunningAsync()
        {
            var entities =
                await _repository
                    .GetByStatusAsync(
                        "Running",
                        500);

            var jobs =
                entities
                    .Select(
                        MapPersistentEntityToDto)
                    .ToList();

            foreach (var job in jobs)
            {
                _jobs[job.JobId] =
                    job;
            }

            return jobs;
        }

        public async Task<List<PropertyMatchSalesAutomationBatchJobDto>>
            GetFailedAsync(
                int limit = 100)
        {
            limit =
                NormalizeLimit(limit);

            var failed =
                await _repository
                    .GetByStatusAsync(
                        "Failed",
                        limit);

            var partial =
                await _repository
                    .GetByStatusAsync(
                        "PartiallyCompleted",
                        limit);

            var jobs =
                failed
                    .Concat(partial)
                    .OrderByDescending(
                        x => x.UpdatedAt)
                    .Take(limit)
                    .Select(
                        MapPersistentEntityToDto)
                    .ToList();

            foreach (var job in jobs)
            {
                _jobs[job.JobId] =
                    job;
            }

            return jobs;
        }

        public async Task<PropertyMatchSalesAutomationBatchJobSummaryDto>
            GetSummaryAsync()
        {
            var persistent =
                await _repository
                    .GetSummaryAsync();

            return new PropertyMatchSalesAutomationBatchJobSummaryDto
            {
                TotalJobs =
                    persistent.TotalJobs,

                RunningJobs =
                    persistent.RunningJobs,

                CompletedJobs =
                    persistent.CompletedJobs,

                PartiallyCompletedJobs =
                    persistent.PartiallyCompletedJobs,

                FailedJobs =
                    persistent.FailedJobs,

                CancelledJobs =
                    persistent.CancelledJobs,

                TotalProcessed =
                    persistent.TotalProcessed,

                TotalSuccessful =
                    persistent.TotalSuccessful,

                TotalFailed =
                    persistent.TotalFailed,

                SuccessRate =
                    persistent.SuccessRate,

                FailureRate =
                    persistent.FailureRate,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        public async Task<bool>
            RemoveAsync(
                Guid jobId)
        {
            _jobs.TryRemove(
                jobId,
                out _);

            return await _repository
                .DeleteAsync(
                    jobId);
        }

        public async Task<int>
            ClearCompletedAsync()
        {
            var removed =
                await _repository
                    .DeleteCompletedAsync();

            var memoryIds =
                _jobs.Values
                    .Where(x =>
                        x.Status == "Completed" ||
                        x.Status == "Cancelled")
                    .Select(
                        x => x.JobId)
                    .ToList();

            foreach (var jobId in memoryIds)
            {
                _jobs.TryRemove(
                    jobId,
                    out _);
            }

            return removed;
        }


        private static PropertyMatchSalesAutomationBatchJobDto
            MapPersistentEntityToDto(
                REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationBatchJob entity)
        {
            PropertyMatchSalesAutomationBatchRuntimeResultDto?
                result = null;

            if (!string.IsNullOrWhiteSpace(entity.ResultJson))
            {
                try
                {
                    result =
                        System.Text.Json.JsonSerializer.Deserialize<
                            PropertyMatchSalesAutomationBatchRuntimeResultDto>(
                                entity.ResultJson);
                }
                catch
                {
                    result = null;
                }
            }

            return new PropertyMatchSalesAutomationBatchJobDto
            {
                JobId = entity.JobId,
                BatchId = entity.BatchId,
                Status = entity.Status,
                Actor = entity.Actor,

                TotalRequested =
                    entity.TotalRequested,

                Processed =
                    entity.Processed,

                Successful =
                    entity.Successful,

                Completed =
                    entity.Completed,

                Failed =
                    entity.Failed,

                AwaitingApproval =
                    entity.AwaitingApproval,

                SuccessRate =
                    entity.SuccessRate,

                FailureRate =
                    entity.FailureRate,

                DurationMilliseconds =
                    entity.DurationMilliseconds,

                ErrorCode =
                    entity.ErrorCode,

                ErrorMessage =
                    entity.ErrorMessage,

                Result =
                    result,

                CreatedAt =
                    entity.CreatedAt,

                StartedAt =
                    entity.StartedAt,

                CompletedAt =
                    entity.CompletedAt,

                UpdatedAt =
                    entity.UpdatedAt
            };
        }

        private static int NormalizeLimit(
            int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(
                limit,
                500);
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

        private async Task CreatePersistentJobAsync(
            PropertyMatchSalesAutomationBatchJobDto job,
            PropertyMatchSalesAutomationBatchRuntimeRequestDto request,
            CancellationToken cancellationToken)
        {
            var entity =
                new PropertyMatchSalesAutomationBatchJob
                {
                    Id =
                        Guid.NewGuid(),

                    JobId =
                        job.JobId,

                    BatchId =
                        job.BatchId,

                    Status =
                        job.Status,

                    Actor =
                        job.Actor,

                    CorrelationId =
                        string.Empty,

                    TotalRequested =
                        job.TotalRequested,

                    Processed =
                        job.Processed,

                    Successful =
                        job.Successful,

                    Completed =
                        job.Completed,

                    Failed =
                        job.Failed,

                    AwaitingApproval =
                        job.AwaitingApproval,

                    SuccessRate =
                        job.SuccessRate,

                    FailureRate =
                        job.FailureRate,

                    DurationMilliseconds =
                        job.DurationMilliseconds,

                    ErrorCode =
                        job.ErrorCode,

                    ErrorMessage =
                        job.ErrorMessage,

                    RequestJson =
                        System.Text.Json.JsonSerializer.Serialize(
                            request),

                    CreatedAt =
                        job.CreatedAt,

                    StartedAt =
                        job.StartedAt,

                    CompletedAt =
                        job.CompletedAt,

                    UpdatedAt =
                        job.UpdatedAt
                };

            await _repository
                .CreateAsync(
                    entity,
                    cancellationToken);
        }


        private async Task UpdatePersistentJobAsync(
            PropertyMatchSalesAutomationBatchJobDto job,
            CancellationToken cancellationToken)
        {
            var entity =
                new PropertyMatchSalesAutomationBatchJob
                {
                    JobId =
                        job.JobId,

                    BatchId =
                        job.BatchId,

                    Status =
                        job.Status,

                    Actor =
                        job.Actor,

                    CorrelationId =
                        string.Empty,

                    TotalRequested =
                        job.TotalRequested,

                    Processed =
                        job.Processed,

                    Successful =
                        job.Successful,

                    Completed =
                        job.Completed,

                    Failed =
                        job.Failed,

                    AwaitingApproval =
                        job.AwaitingApproval,

                    SuccessRate =
                        job.SuccessRate,

                    FailureRate =
                        job.FailureRate,

                    DurationMilliseconds =
                        job.DurationMilliseconds,

                    ErrorCode =
                        job.ErrorCode,

                    ErrorMessage =
                        job.ErrorMessage,

                    ResultJson =
                        job.Result == null
                            ? null
                            : System.Text.Json.JsonSerializer.Serialize(
                                job.Result),

                    CreatedAt =
                        job.CreatedAt,

                    StartedAt =
                        job.StartedAt,

                    CompletedAt =
                        job.CompletedAt,

                    UpdatedAt =
                        job.UpdatedAt
                };

            await _repository
                .UpdateAsync(
                    entity,
                    cancellationToken);
        }

        private async Task UpdatePersistentJobSafeAsync(
            PropertyMatchSalesAutomationBatchJobDto job)
        {
            try
            {
                await UpdatePersistentJobAsync(
                    job,
                    CancellationToken.None);
            }
            catch
            {
                // Ana batch hatasını persistence hatasıyla değiştirme.
            }
        }

    }

    public class PropertyMatchSalesAutomationBatchJobDto
    {
        public Guid JobId { get; set; }

        public Guid? BatchId { get; set; }

        public string Status { get; set; }
            = "Pending";

        public string Actor { get; set; }
            = "System";

        public int TotalRequested { get; set; }

        public int Processed { get; set; }

        public int Successful { get; set; }

        public int Completed { get; set; }

        public int Failed { get; set; }

        public int AwaitingApproval { get; set; }

        public decimal SuccessRate { get; set; }

        public decimal FailureRate { get; set; }

        public long DurationMilliseconds { get; set; }

        public string? ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public PropertyMatchSalesAutomationBatchRuntimeResultDto?
            Result { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
            = DateTime.UtcNow;
    }

    public class PropertyMatchSalesAutomationBatchJobSummaryDto
    {
        public int TotalJobs { get; set; }

        public int RunningJobs { get; set; }

        public int CompletedJobs { get; set; }

        public int PartiallyCompletedJobs { get; set; }

        public int FailedJobs { get; set; }

        public int CancelledJobs { get; set; }

        public int TotalProcessed { get; set; }

        public int TotalSuccessful { get; set; }

        public int TotalFailed { get; set; }

        public decimal SuccessRate { get; set; }

        public decimal FailureRate { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
