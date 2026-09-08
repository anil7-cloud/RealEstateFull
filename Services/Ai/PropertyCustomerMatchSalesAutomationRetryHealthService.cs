namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryHealthService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationBatchJobRepository
                _jobRepository;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryEventRepository
                _eventRepository;

        public PropertyCustomerMatchSalesAutomationRetryHealthService(
            PropertyCustomerMatchSalesAutomationBatchJobRepository jobRepository,
            PropertyCustomerMatchSalesAutomationRetryEventRepository eventRepository)
        {
            _jobRepository =
                jobRepository;

            _eventRepository =
                eventRepository;
        }

        public async Task<PropertyMatchSalesAutomationRetryHealthDto>
            GetHealthAsync(
                CancellationToken cancellationToken = default)
        {
            var issues =
                new List<string>();

            var databaseAvailable =
                await _jobRepository
                    .CanConnectAsync(
                        cancellationToken);

            if (!databaseAvailable)
            {
                issues.Add(
                    "Batch job database connection unavailable.");
            }

            var eventDatabaseAvailable =
                await _eventRepository
                    .CanConnectAsync(
                        cancellationToken);

            if (!eventDatabaseAvailable)
            {
                issues.Add(
                    "Retry event database connection unavailable.");
            }

            var failed =
                databaseAvailable
                    ? await _jobRepository
                        .GetByStatusAsync(
                            "Failed",
                            1000,
                            cancellationToken)
                    : new();

            var interrupted =
                databaseAvailable
                    ? await _jobRepository
                        .GetByStatusAsync(
                            "Interrupted",
                            1000,
                            cancellationToken)
                    : new();

            var now =
                DateTime.UtcNow;

            var retryCandidates =
                failed
                    .Concat(interrupted)
                    .ToList();

            var readyForRetry =
                retryCandidates.Count(x =>
                    x.RetryCount < 3 &&
                    x.NextRetryAt.HasValue &&
                    x.NextRetryAt.Value <= now &&
                    !x.RetryClaimed);

            var claimed =
                retryCandidates.Count(x =>
                    x.RetryClaimed);

            var staleClaimThreshold =
                now.AddMinutes(-10);

            var staleClaims =
                retryCandidates.Count(x =>
                    x.RetryClaimed &&
                    x.RetryClaimedAt.HasValue &&
                    x.RetryClaimedAt.Value <=
                        staleClaimThreshold);

            if (staleClaims > 0)
            {
                issues.Add(
                    $"{staleClaims} stale retry claim(s) detected.");
            }

            var eventSummary =
                eventDatabaseAvailable
                    ? await _eventRepository
                        .GetSummaryAsync(
                            cancellationToken)
                    : null;

            if (eventSummary != null &&
                eventSummary.RetryAttempts >= 10 &&
                eventSummary.FailureRate >= 50m)
            {
                issues.Add(
                    $"Retry failure rate is high: {eventSummary.FailureRate}%.");
            }

            if (eventSummary != null &&
                eventSummary.LeaseLost > 0)
            {
                issues.Add(
                    $"Lease loss detected: {eventSummary.LeaseLost}.");
            }

            var healthy =
                databaseAvailable &&
                eventDatabaseAvailable &&
                staleClaims == 0 &&
                (
                    eventSummary == null ||
                    eventSummary.RetryAttempts < 10 ||
                    eventSummary.FailureRate < 50m
                );

            return new()
            {
                Healthy =
                    healthy,

                Status =
                    healthy
                        ? "Healthy"
                        : databaseAvailable &&
                          eventDatabaseAvailable
                            ? "Degraded"
                            : "Unhealthy",

                DatabaseAvailable =
                    databaseAvailable,

                EventDatabaseAvailable =
                    eventDatabaseAvailable,

                FailedJobs =
                    failed.Count,

                InterruptedJobs =
                    interrupted.Count,

                ReadyForRetry =
                    readyForRetry,

                ClaimedRetries =
                    claimed,

                StaleClaims =
                    staleClaims,

                RetryAttempts =
                    eventSummary?.RetryAttempts ?? 0,

                RetrySucceeded =
                    eventSummary?.RetrySucceeded ?? 0,

                RetryFailed =
                    eventSummary?.RetryFailed ?? 0,

                RetrySuccessRate =
                    eventSummary?.SuccessRate ?? 0,

                RetryFailureRate =
                    eventSummary?.FailureRate ?? 0,

                LeaseLost =
                    eventSummary?.LeaseLost ?? 0,

                IssueCount =
                    issues.Count,

                Issues =
                    issues,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }
    }

    public class PropertyMatchSalesAutomationRetryHealthDto
    {
        public bool Healthy { get; set; }

        public string Status { get; set; }
            = string.Empty;

        public bool DatabaseAvailable { get; set; }

        public bool EventDatabaseAvailable { get; set; }

        public int FailedJobs { get; set; }

        public int InterruptedJobs { get; set; }

        public int ReadyForRetry { get; set; }

        public int ClaimedRetries { get; set; }

        public int StaleClaims { get; set; }

        public int RetryAttempts { get; set; }

        public int RetrySucceeded { get; set; }

        public int RetryFailed { get; set; }

        public decimal RetrySuccessRate { get; set; }

        public decimal RetryFailureRate { get; set; }

        public int LeaseLost { get; set; }

        public int IssueCount { get; set; }

        public List<string> Issues { get; set; }
            = new();

        public DateTime GeneratedAt { get; set; }
    }
}
