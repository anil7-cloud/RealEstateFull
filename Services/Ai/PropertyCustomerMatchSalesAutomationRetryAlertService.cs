namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryAlertService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationRetryHealthService
                _healthService;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryAlertRepository
                _alertRepository;

        public PropertyCustomerMatchSalesAutomationRetryAlertService(
            PropertyCustomerMatchSalesAutomationRetryHealthService healthService,
            PropertyCustomerMatchSalesAutomationRetryAlertRepository alertRepository)
        {
            _healthService =
                healthService;

            _alertRepository =
                alertRepository;
        }

        public async Task<PropertyMatchSalesAutomationRetryAlertSummaryDto>
            GetAlertsAsync(
                CancellationToken cancellationToken = default)
        {
            var health =
                await _healthService
                    .GetHealthAsync(
                        cancellationToken);

            var alerts =
                new List<PropertyMatchSalesAutomationRetryAlertDto>();

            if (!health.DatabaseAvailable)
            {
                alerts.Add(
                    CreateAlert(
                        "DatabaseUnavailable",
                        "Critical",
                        "Batch job database is unavailable.",
                        "Database"));
            }

            if (!health.EventDatabaseAvailable)
            {
                alerts.Add(
                    CreateAlert(
                        "EventDatabaseUnavailable",
                        "Critical",
                        "Retry event database is unavailable.",
                        "Database"));
            }

            if (health.StaleClaims > 0)
            {
                alerts.Add(
                    CreateAlert(
                        "StaleClaims",
                        health.StaleClaims >= 5
                            ? "Critical"
                            : "Warning",
                        $"{health.StaleClaims} stale retry claim(s) detected.",
                        "Claim"));
            }

            if (health.RetryAttempts >= 10 &&
                health.RetryFailureRate >= 50m)
            {
                alerts.Add(
                    CreateAlert(
                        "HighFailureRate",
                        health.RetryFailureRate >= 75m
                            ? "Critical"
                            : "Warning",
                        $"Retry failure rate is {health.RetryFailureRate}%.",
                        "Retry"));
            }

            if (health.LeaseLost > 0)
            {
                alerts.Add(
                    CreateAlert(
                        "LeaseLost",
                        health.LeaseLost >= 5
                            ? "Critical"
                            : "Warning",
                        $"{health.LeaseLost} retry lease loss event(s) detected.",
                        "Lease"));
            }

            if (health.ReadyForRetry >= 25)
            {
                alerts.Add(
                    CreateAlert(
                        "RetryBacklog",
                        health.ReadyForRetry >= 100
                            ? "Critical"
                            : "Warning",
                        $"{health.ReadyForRetry} job(s) are waiting for retry.",
                        "Queue"));
            }

            var criticalCount =
                alerts.Count(x =>
                    x.Severity == "Critical");

            var warningCount =
                alerts.Count(x =>
                    x.Severity == "Warning");

            await SynchronizePersistentAlertsAsync(
                alerts,
                cancellationToken);

            return new()
            {
                Status =
                    criticalCount > 0
                        ? "Critical"
                        : warningCount > 0
                            ? "Warning"
                            : "Healthy",

                HasAlerts =
                    alerts.Count > 0,

                TotalAlerts =
                    alerts.Count,

                CriticalAlerts =
                    criticalCount,

                WarningAlerts =
                    warningCount,

                Alerts =
                    alerts,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private async Task SynchronizePersistentAlertsAsync(
            List<PropertyMatchSalesAutomationRetryAlertDto> currentAlerts,
            CancellationToken cancellationToken)
        {
            var activeCodes =
                currentAlerts
                    .Select(x => x.Code)
                    .ToHashSet(
                        StringComparer.OrdinalIgnoreCase);

            foreach (var alert in currentAlerts)
            {
                await _alertRepository
                    .CreateAsync(
                        new REAL_ESTATE_CLEAN.Core.Domain.Entities
                            .PropertyMatchSalesAutomationRetryAlert
                        {
                            Id =
                                Guid.NewGuid(),

                            Code =
                                alert.Code,

                            Severity =
                                alert.Severity,

                            Category =
                                alert.Category,

                            Message =
                                alert.Message,

                            Status =
                                "Active",

                            CreatedAt =
                                DateTime.UtcNow,

                            UpdatedAt =
                                DateTime.UtcNow
                        },
                        cancellationToken);
            }

            var persistedAlerts =
                await _alertRepository
                    .GetActiveAsync(
                        1000,
                        cancellationToken);

            foreach (var persisted in persistedAlerts)
            {
                if (!activeCodes.Contains(
                        persisted.Code))
                {
                    await _alertRepository
                        .ResolveAsync(
                            persisted.Id,
                            "System",
                            cancellationToken);
                }
            }
        }

        private static PropertyMatchSalesAutomationRetryAlertDto
            CreateAlert(
                string code,
                string severity,
                string message,
                string category)
        {
            return new()
            {
                Id =
                    Guid.NewGuid(),

                Code =
                    code,

                Severity =
                    severity,

                Category =
                    category,

                Message =
                    message,

                CreatedAt =
                    DateTime.UtcNow
            };
        }
    }

    public class PropertyMatchSalesAutomationRetryAlertDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public string Category { get; set; }
            = string.Empty;

        public string Message { get; set; }
            = string.Empty;

        public DateTime CreatedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationRetryAlertSummaryDto
    {
        public string Status { get; set; }
            = string.Empty;

        public bool HasAlerts { get; set; }

        public int TotalAlerts { get; set; }

        public int CriticalAlerts { get; set; }

        public int WarningAlerts { get; set; }

        public List<PropertyMatchSalesAutomationRetryAlertDto>
            Alerts { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }
}
