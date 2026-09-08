using System.Text.Json;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationBatchJobRetryService
    {
        private const int MaxRetryCount = 3;
        private readonly
            PropertyCustomerMatchSalesAutomationBatchJobRepository
                _repository;

        private readonly
            PropertyCustomerMatchSalesAutomationBatchJobService
                _batchJobService;

        public PropertyCustomerMatchSalesAutomationBatchJobRetryService(
            PropertyCustomerMatchSalesAutomationBatchJobRepository repository,
            PropertyCustomerMatchSalesAutomationBatchJobService batchJobService)
        {
            _repository = repository;
            _batchJobService = batchJobService;
        }

        public async Task<
            PropertyMatchSalesAutomationBatchJobRetryResultDto>
            RetryAsync(
                Guid jobId,
                CancellationToken cancellationToken = default)
        {
            var source =
                await _repository.GetByJobIdAsync(
                    jobId,
                    cancellationToken);

            if (source == null)
            {
                return new()
                {
                    Successful = false,
                    Status = "NotFound",
                    SourceJobId = jobId,
                    ErrorMessage = "Batch job bulunamadı.",
                    GeneratedAt = DateTime.UtcNow
                };
            }

            var retryDelayMinutes =
                source.RetryCount switch
                {
                    0 => 1,
                    1 => 2,
                    _ => 4
                };

            var calculatedNextRetryAt =
                source.UpdatedAt.AddMinutes(
                    retryDelayMinutes);

            var nextRetryAt =
                source.NextRetryAt.HasValue &&
                source.NextRetryAt.Value > calculatedNextRetryAt
                    ? source.NextRetryAt.Value
                    : calculatedNextRetryAt;

            if (DateTime.UtcNow < nextRetryAt)
            {
                source.NextRetryAt =
                    nextRetryAt;

                source.UpdatedAt =
                    DateTime.UtcNow;

                await _repository.UpdateAsync(
                    source,
                    cancellationToken);

                return new()
                {
                    Successful = false,
                    Status = "RetryBackoffActive",
                    SourceJobId = jobId,
                    NextRetryAt = nextRetryAt,
                    ErrorMessage =
                        $"Retry henüz hazır değil. Sonraki deneme: {nextRetryAt:O}",
                    GeneratedAt = DateTime.UtcNow
                };
            }

            if (source.RetryCount >= MaxRetryCount)
            {
                return new()
                {
                    Successful = false,
                    Status = "RetryLimitExceeded",
                    SourceJobId = jobId,
                    ErrorMessage =
                        $"Maksimum retry sayısına ulaşıldı: {MaxRetryCount}.",
                    GeneratedAt = DateTime.UtcNow
                };
            }

            var retryHistory =
                await _repository
                    .GetRetryHistoryAsync(
                        jobId,
                        cancellationToken);

            var successfulRetryExists =
                retryHistory.Any(x =>
                    x.ParentJobId == source.JobId &&
                    x.Status == "Completed");

            if (successfulRetryExists)
            {
                return new()
                {
                    Successful = false,
                    Status = "AlreadySuccessfullyRetried",
                    SourceJobId = jobId,
                    ErrorMessage =
                        "Bu job için başarılı bir retry zaten mevcut.",
                    GeneratedAt = DateTime.UtcNow
                };
            }

            if (source.Status != "Interrupted" &&
                source.Status != "Failed")
            {
                return new()
                {
                    Successful = false,
                    Status = "NotRetryable",
                    SourceJobId = jobId,
                    ErrorMessage =
                        $"'{source.Status}' durumundaki job retry edilemez.",
                    GeneratedAt = DateTime.UtcNow
                };
            }

            if (string.IsNullOrWhiteSpace(source.RequestJson))
            {
                return new()
                {
                    Successful = false,
                    Status = "RequestUnavailable",
                    SourceJobId = jobId,
                    ErrorMessage =
                        "Orijinal RequestJson bulunamadı.",
                    GeneratedAt = DateTime.UtcNow
                };
            }

            PropertyMatchSalesAutomationBatchRuntimeRequestDto?
                request;

            try
            {
                request =
                    JsonSerializer.Deserialize<
                        PropertyMatchSalesAutomationBatchRuntimeRequestDto>(
                            source.RequestJson);
            }
            catch (Exception ex)
            {
                return new()
                {
                    Successful = false,
                    Status = "InvalidRequestJson",
                    SourceJobId = jobId,
                    ErrorMessage = ex.Message,
                    GeneratedAt = DateTime.UtcNow
                };
            }

            if (request == null)
            {
                return new()
                {
                    Successful = false,
                    Status = "InvalidRequestJson",
                    SourceJobId = jobId,
                    ErrorMessage =
                        "RequestJson deserialize edilemedi.",
                    GeneratedAt = DateTime.UtcNow
                };
            }

            try
            {
                var newJob =
                    await _batchJobService.CreateAndRunAsync(
                        request,
                        cancellationToken);

                var persistentNewJob =
                    await _repository.GetByJobIdAsync(
                        newJob.JobId,
                        cancellationToken);

                if (persistentNewJob != null)
                {
                    persistentNewJob.ParentJobId =
                        source.JobId;

                    persistentNewJob.RetryCount =
                        source.RetryCount + 1;

                    var nextDelayMinutes =
                        persistentNewJob.RetryCount switch
                        {
                            1 => 2,
                            _ => 4
                        };

                    persistentNewJob.NextRetryAt =
                        DateTime.UtcNow.AddMinutes(
                            nextDelayMinutes);

                    persistentNewJob.UpdatedAt =
                        DateTime.UtcNow;

                    await _repository.UpdateAsync(
                        persistentNewJob,
                        cancellationToken);
                }

                return new()
                {
                    Successful =
                        newJob.Status == "Completed" ||
                        newJob.Status == "PartiallyCompleted",

                    Status = "Retried",

                    SourceJobId =
                        source.JobId,

                    NewJobId =
                        newJob.JobId,

                    NewBatchId =
                        newJob.BatchId,

                    NewJobStatus =
                        newJob.Status,

                    GeneratedAt =
                        DateTime.UtcNow
                };
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return new()
                {
                    Successful = false,
                    Status = "RetryFailed",
                    SourceJobId = jobId,
                    ErrorMessage = ex.Message,
                    GeneratedAt = DateTime.UtcNow
                };
            }
        }
    }

    public class PropertyMatchSalesAutomationBatchJobRetryResultDto
    {
        public bool Successful { get; set; }

        public string Status { get; set; } =
            "Pending";

        public Guid SourceJobId { get; set; }

        public Guid? NewJobId { get; set; }

        public Guid? NewBatchId { get; set; }

        public string? NewJobStatus { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime? NextRetryAt { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
