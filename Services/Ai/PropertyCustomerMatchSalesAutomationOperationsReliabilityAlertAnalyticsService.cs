using REAL_ESTATE_CLEAN.Core.Domain.Entities;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertAnalyticsService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertRepository
                _alertRepository;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertAnalyticsService(
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertRepository alertRepository)
        {
            _alertRepository =
                alertRepository;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsReliabilityAlertAnalyticsDto>
            GetAnalyticsAsync(
                CancellationToken cancellationToken = default)
        {
            var now =
                DateTime.UtcNow;

            /*
             * Repository şu anda GetSinceAsync içermediği için
             * maksimum 5000 recent alert alıp zaman pencerelerinde
             * filtreliyoruz.
             */
            var alerts =
                await _alertRepository
                    .GetRecentAsync(
                        5000,
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

        private static
            PropertyMatchSalesAutomationOperationsReliabilityAlertAnalyticsWindowDto
            BuildWindow(
                IEnumerable<
                    PropertyMatchSalesAutomationOperationsReliabilityAlert>
                    source,
                DateTime from,
                DateTime to)
        {
            var alerts =
                source
                    .Where(x =>
                        x.CreatedAt >= from &&
                        x.CreatedAt <= to)
                    .ToList();

            var p1 =
                alerts.Count(x =>
                    string.Equals(
                        x.Priority,
                        "P1",
                        StringComparison.OrdinalIgnoreCase));

            var p2 =
                alerts.Count(x =>
                    string.Equals(
                        x.Priority,
                        "P2",
                        StringComparison.OrdinalIgnoreCase));

            var open =
                alerts.Count(x =>
                    string.Equals(
                        x.Status,
                        "Open",
                        StringComparison.OrdinalIgnoreCase));

            var acknowledged =
                alerts.Count(x =>
                    string.Equals(
                        x.Status,
                        "Acknowledged",
                        StringComparison.OrdinalIgnoreCase));

            var resolved =
                alerts.Count(x =>
                    string.Equals(
                        x.Status,
                        "Resolved",
                        StringComparison.OrdinalIgnoreCase));

            var acknowledgedAlerts =
                alerts
                    .Where(x =>
                        x.AcknowledgedAt.HasValue &&
                        x.AcknowledgedAt.Value >=
                            x.CreatedAt)
                    .ToList();

            decimal mttaMinutes =
                acknowledgedAlerts.Count == 0
                    ? 0m
                    : Math.Round(
                        (decimal)
                        acknowledgedAlerts.Average(x =>
                            (
                                x.AcknowledgedAt!.Value -
                                x.CreatedAt
                            ).TotalMinutes),
                        2);

            var resolvedAlerts =
                alerts
                    .Where(x =>
                        x.ResolvedAt.HasValue &&
                        x.ResolvedAt.Value >=
                            x.CreatedAt)
                    .ToList();

            decimal mttrMinutes =
                resolvedAlerts.Count == 0
                    ? 0m
                    : Math.Round(
                        (decimal)
                        resolvedAlerts.Average(x =>
                            (
                                x.ResolvedAt!.Value -
                                x.CreatedAt
                            ).TotalMinutes),
                        2);

            var maximumResolutionMinutes =
                resolvedAlerts.Count == 0
                    ? 0m
                    : Math.Round(
                        (decimal)
                        resolvedAlerts.Max(x =>
                            (
                                x.ResolvedAt!.Value -
                                x.CreatedAt
                            ).TotalMinutes),
                        2);

            var resolutionRate =
                alerts.Count == 0
                    ? 0m
                    : Math.Round(
                        resolved *
                        100m /
                        alerts.Count,
                        2);

            return new()
            {
                From =
                    from,

                To =
                    to,

                TotalAlerts =
                    alerts.Count,

                P1Count =
                    p1,

                P2Count =
                    p2,

                OpenCount =
                    open,

                AcknowledgedCount =
                    acknowledged,

                ResolvedCount =
                    resolved,

                MttaMinutes =
                    mttaMinutes,

                MttrMinutes =
                    mttrMinutes,

                MaximumResolutionMinutes =
                    maximumResolutionMinutes,

                ResolutionRate =
                    resolutionRate
            };
        }
    }

    public class
        PropertyMatchSalesAutomationOperationsReliabilityAlertAnalyticsDto
    {
        public
            PropertyMatchSalesAutomationOperationsReliabilityAlertAnalyticsWindowDto
            Last24Hours { get; set; } = new();

        public
            PropertyMatchSalesAutomationOperationsReliabilityAlertAnalyticsWindowDto
            Last7Days { get; set; } = new();

        public
            PropertyMatchSalesAutomationOperationsReliabilityAlertAnalyticsWindowDto
            Last30Days { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class
        PropertyMatchSalesAutomationOperationsReliabilityAlertAnalyticsWindowDto
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int TotalAlerts { get; set; }

        public int P1Count { get; set; }

        public int P2Count { get; set; }

        public int OpenCount { get; set; }

        public int AcknowledgedCount { get; set; }

        public int ResolvedCount { get; set; }

        public decimal MttaMinutes { get; set; }

        public decimal MttrMinutes { get; set; }

        public decimal MaximumResolutionMinutes { get; set; }

        public decimal ResolutionRate { get; set; }
    }
}
