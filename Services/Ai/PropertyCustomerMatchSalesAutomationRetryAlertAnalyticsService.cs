namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryAlertAnalyticsService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationRetryAlertRepository
                _repository;

        public PropertyCustomerMatchSalesAutomationRetryAlertAnalyticsService(
            PropertyCustomerMatchSalesAutomationRetryAlertRepository repository)
        {
            _repository =
                repository;
        }

        public async Task<PropertyMatchSalesAutomationRetryAlertAnalyticsDto>
            GetAnalyticsAsync(
                CancellationToken cancellationToken = default)
        {
            var now =
                DateTime.UtcNow;

            var alerts =
                await _repository
                    .GetCreatedSinceAsync(
                        now.AddDays(-30),
                        10000,
                        cancellationToken);

            return new()
            {
                Last24Hours =
                    BuildWindow(
                        alerts,
                        now.AddHours(-24),
                        now),

                Last7Days =
                    BuildWindow(
                        alerts,
                        now.AddDays(-7),
                        now),

                Last30Days =
                    BuildWindow(
                        alerts,
                        now.AddDays(-30),
                        now),

                GeneratedAt =
                    now
            };
        }

        public async Task<PropertyMatchSalesAutomationRetryAlertTrendDto>
            GetTrendAsync(
                int days = 7,
                CancellationToken cancellationToken = default)
        {
            days =
                Math.Clamp(
                    days,
                    1,
                    30);

            var now =
                DateTime.UtcNow;

            var currentStart =
                now.AddDays(-days);

            var previousStart =
                now.AddDays(-(days * 2));

            var alerts =
                await _repository
                    .GetCreatedSinceAsync(
                        previousStart,
                        10000,
                        cancellationToken);

            var current =
                alerts.Count(x =>
                    x.CreatedAt >= currentStart &&
                    x.CreatedAt <= now);

            var previous =
                alerts.Count(x =>
                    x.CreatedAt >= previousStart &&
                    x.CreatedAt < currentStart);

            decimal changePercent;

            if (previous == 0)
            {
                changePercent =
                    current == 0
                        ? 0m
                        : 100m;
            }
            else
            {
                changePercent =
                    Math.Round(
                        ((decimal)current - previous) /
                        previous *
                        100m,
                        2);
            }

            string trend;

            if (changePercent <= -10m)
            {
                trend =
                    "Improving";
            }
            else if (changePercent >= 10m)
            {
                trend =
                    "Worsening";
            }
            else
            {
                trend =
                    "Stable";
            }

            var currentCritical =
                alerts.Count(x =>
                    x.CreatedAt >= currentStart &&
                    x.CreatedAt <= now &&
                    x.Severity == "Critical");

            var previousCritical =
                alerts.Count(x =>
                    x.CreatedAt >= previousStart &&
                    x.CreatedAt < currentStart &&
                    x.Severity == "Critical");

            return new()
            {
                Days =
                    days,

                Trend =
                    trend,

                CurrentPeriodAlerts =
                    current,

                PreviousPeriodAlerts =
                    previous,

                AlertChangePercent =
                    changePercent,

                CurrentCriticalAlerts =
                    currentCritical,

                PreviousCriticalAlerts =
                    previousCritical,

                CurrentPeriodStart =
                    currentStart,

                PreviousPeriodStart =
                    previousStart,

                GeneratedAt =
                    now
            };
        }

        private static PropertyMatchSalesAutomationRetryAlertWindowDto
            BuildWindow(
                IEnumerable<
                    REAL_ESTATE_CLEAN.Core.Domain.Entities
                        .PropertyMatchSalesAutomationRetryAlert> source,
                DateTime from,
                DateTime to)
        {
            var alerts =
                source
                    .Where(x =>
                        x.CreatedAt >= from &&
                        x.CreatedAt <= to)
                    .ToList();

            var resolved =
                alerts
                    .Where(x =>
                        x.Status == "Resolved")
                    .ToList();

            var resolutionTimes =
                resolved
                    .Where(x =>
                        x.ResolvedAt.HasValue &&
                        x.ResolvedAt.Value >= x.CreatedAt)
                    .Select(x =>
                        (
                            x.ResolvedAt!.Value -
                            x.CreatedAt
                        ).TotalMinutes)
                    .ToList();

            var mostFrequentCode =
                alerts
                    .GroupBy(x =>
                        x.Code)
                    .OrderByDescending(x =>
                        x.Count())
                    .Select(x =>
                        x.Key)
                    .FirstOrDefault();

            var critical =
                alerts.Count(x =>
                    x.Severity == "Critical");

            var warning =
                alerts.Count(x =>
                    x.Severity == "Warning");

            var active =
                alerts.Count(x =>
                    x.Status == "Active");

            var acknowledged =
                alerts.Count(x =>
                    x.Status == "Acknowledged");

            return new()
            {
                From =
                    from,

                To =
                    to,

                TotalAlerts =
                    alerts.Count,

                CriticalAlerts =
                    critical,

                WarningAlerts =
                    warning,

                ActiveAlerts =
                    active,

                AcknowledgedAlerts =
                    acknowledged,

                ResolvedAlerts =
                    resolved.Count,

                CriticalRate =
                    Percentage(
                        critical,
                        alerts.Count),

                ResolutionRate =
                    Percentage(
                        resolved.Count,
                        alerts.Count),

                AverageResolutionMinutes =
                    resolutionTimes.Count == 0
                        ? 0
                        : Math.Round(
                            (decimal)resolutionTimes.Average(),
                            2),

                MostFrequentAlertCode =
                    mostFrequentCode
            };
        }

        private static decimal Percentage(
            int value,
            int total)
        {
            if (total <= 0)
            {
                return 0;
            }

            return Math.Round(
                (decimal)value /
                total *
                100m,
                2);
        }
    }

    public class PropertyMatchSalesAutomationRetryAlertAnalyticsDto
    {
        public PropertyMatchSalesAutomationRetryAlertWindowDto
            Last24Hours { get; set; } = new();

        public PropertyMatchSalesAutomationRetryAlertWindowDto
            Last7Days { get; set; } = new();

        public PropertyMatchSalesAutomationRetryAlertWindowDto
            Last30Days { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationRetryAlertWindowDto
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int TotalAlerts { get; set; }

        public int CriticalAlerts { get; set; }

        public int WarningAlerts { get; set; }

        public int ActiveAlerts { get; set; }

        public int AcknowledgedAlerts { get; set; }

        public int ResolvedAlerts { get; set; }

        public decimal CriticalRate { get; set; }

        public decimal ResolutionRate { get; set; }

        public decimal AverageResolutionMinutes { get; set; }

        public string? MostFrequentAlertCode { get; set; }
    }

    public class PropertyMatchSalesAutomationRetryAlertTrendDto
    {
        public int Days { get; set; }

        public string Trend { get; set; }
            = "Stable";

        public int CurrentPeriodAlerts { get; set; }

        public int PreviousPeriodAlerts { get; set; }

        public decimal AlertChangePercent { get; set; }

        public int CurrentCriticalAlerts { get; set; }

        public int PreviousCriticalAlerts { get; set; }

        public DateTime CurrentPeriodStart { get; set; }

        public DateTime PreviousPeriodStart { get; set; }

        public DateTime GeneratedAt { get; set; }
    }

}
