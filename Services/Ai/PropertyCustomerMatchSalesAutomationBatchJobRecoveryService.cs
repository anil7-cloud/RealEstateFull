using REAL_ESTATE_CLEAN.Core.Domain.Entities;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationBatchJobRecoveryService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationBatchJobRepository
                _repository;

        public PropertyCustomerMatchSalesAutomationBatchJobRecoveryService(
            PropertyCustomerMatchSalesAutomationBatchJobRepository repository)
        {
            _repository = repository;
        }

        public async Task<
            PropertyMatchSalesAutomationBatchJobRecoveryResultDto>
            RecoverAsync(
                CancellationToken cancellationToken = default)
        {
            var startedAt =
                DateTime.UtcNow;

            var result =
                new PropertyMatchSalesAutomationBatchJobRecoveryResultDto
                {
                    StartedAt =
                        startedAt
                };

            var canConnect =
                await _repository
                    .CanConnectAsync(
                        cancellationToken);

            if (!canConnect)
            {
                result.Successful =
                    false;

                result.Status =
                    "DatabaseUnavailable";

                result.ErrorMessage =
                    "Batch job veritabanına bağlanılamadı.";

                result.CompletedAt =
                    DateTime.UtcNow;

                return result;
            }

            try
            {
                var runningJobs =
                    await _repository
                        .GetByStatusAsync(
                            "Running",
                            1000,
                            cancellationToken);

                result.RunningJobsFound =
                    runningJobs.Count;

                foreach (var job in runningJobs)
                {
                    cancellationToken
                        .ThrowIfCancellationRequested();

                    try
                    {
                        MarkInterrupted(
                            job);

                        var updated =
                            await _repository
                                .UpdateAsync(
                                    job,
                                    cancellationToken);

                        if (updated != null)
                        {
                            result.RecoveredJobs++;
                        }
                        else
                        {
                            result.FailedJobs++;

                            result.Errors.Add(
                                $"Job {job.JobId}: kayıt bulunamadı.");
                        }
                    }
                    catch (Exception ex)
                    {
                        result.FailedJobs++;

                        result.Errors.Add(
                            $"Job {job.JobId}: {ex.Message}");
                    }
                }

                result.Successful =
                    result.FailedJobs == 0;

                result.Status =
                    result.FailedJobs == 0
                        ? "Completed"
                        : result.RecoveredJobs > 0
                            ? "PartiallyCompleted"
                            : "Failed";

                result.CompletedAt =
                    DateTime.UtcNow;

                return result;
            }
            catch (OperationCanceledException)
            {
                result.Successful =
                    false;

                result.Status =
                    "Cancelled";

                result.ErrorMessage =
                    "Batch job recovery iptal edildi.";

                result.CompletedAt =
                    DateTime.UtcNow;

                throw;
            }
            catch (Exception ex)
            {
                result.Successful =
                    false;

                result.Status =
                    "Failed";

                result.ErrorMessage =
                    ex.Message;

                result.CompletedAt =
                    DateTime.UtcNow;

                return result;
            }
        }

        private static void MarkInterrupted(
            PropertyMatchSalesAutomationBatchJob job)
        {
            var now =
                DateTime.UtcNow;

            job.Status =
                "Interrupted";

            job.ErrorCode =
                "ApplicationRestart";

            job.ErrorMessage =
                "Uygulama yeniden başlatıldığı için çalışan batch job kesildi.";

            job.CompletedAt =
                now;

            job.UpdatedAt =
                now;

            if (job.StartedAt.HasValue)
            {
                var duration =
                    now - job.StartedAt.Value;

                job.DurationMilliseconds =
                    Math.Max(
                        0L,
                        (long)duration.TotalMilliseconds);
            }
        }
    }

    public class PropertyMatchSalesAutomationBatchJobRecoveryResultDto
    {
        public bool Successful { get; set; }

        public string Status { get; set; }
            = "Pending";

        public int RunningJobsFound { get; set; }

        public int RecoveredJobs { get; set; }

        public int FailedJobs { get; set; }

        public string? ErrorMessage { get; set; }

        public List<string> Errors { get; set; }
            = new();

        public DateTime StartedAt { get; set; }

        public DateTime CompletedAt { get; set; }
    }
}
