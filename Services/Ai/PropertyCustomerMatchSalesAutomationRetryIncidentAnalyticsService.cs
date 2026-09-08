using REAL_ESTATE_CLEAN.Core.Domain.Entities;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryIncidentAnalyticsService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationRetryIncidentRepository
                _repository;

        public PropertyCustomerMatchSalesAutomationRetryIncidentAnalyticsService(
            PropertyCustomerMatchSalesAutomationRetryIncidentRepository repository)
        {
            _repository =
                repository;
        }

        public async Task<PropertyMatchSalesAutomationRetryIncidentAnalyticsDto>
            GetAnalyticsAsync(
                CancellationToken cancellationToken = default)
        {
            var now =
                DateTime.UtcNow;

            var incidents =
                await _repository
                    .GetCreatedSinceAsync(
                        now.AddDays(-30),
                        20000,
                        cancellationToken);

            return new()
            {
                Last24Hours =
                    BuildWindow(
                        incidents,
                        now.AddHours(-24),
                        now),

                Last7Days =
                    BuildWindow(
                        incidents,
                        now.AddDays(-7),
                        now),

                Last30Days =
                    BuildWindow(
                        incidents,
                        now.AddDays(-30),
                        now),

                GeneratedAt =
                    now
            };
        }

        private static PropertyMatchSalesAutomationRetryIncidentWindowDto
            BuildWindow(
                IEnumerable<PropertyMatchSalesAutomationRetryIncident> source,
                DateTime from,
                DateTime to)
        {
            var incidents =
                source
                    .Where(x =>
                        x.CreatedAt >= from &&
                        x.CreatedAt <= to)
                    .ToList();

            var resolved =
                incidents
                    .Where(x =>
                        string.Equals(
                            x.Status,
                            "Resolved",
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();

            var open =
                incidents.Count(x =>
                    string.Equals(
                        x.Status,
                        "Open",
                        StringComparison.OrdinalIgnoreCase));

            var acknowledged =
                incidents.Count(x =>
                    string.Equals(
                        x.Status,
                        "Acknowledged",
                        StringComparison.OrdinalIgnoreCase));

            var critical =
                incidents.Count(x =>
                    string.Equals(
                        x.Severity,
                        "Critical",
                        StringComparison.OrdinalIgnoreCase));

            var high =
                incidents.Count(x =>
                    string.Equals(
                        x.Severity,
                        "High",
                        StringComparison.OrdinalIgnoreCase));

            var resolutionMinutes =
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

            var mttr =
                resolutionMinutes.Count == 0
                    ? 0m
                    : Math.Round(
                        (decimal)resolutionMinutes.Average(),
                        2);

            var maxRisk =
                incidents.Count == 0
                    ? 0m
                    : incidents.Max(x =>
                        x.RiskScore);

            var averageRisk =
                incidents.Count == 0
                    ? 0m
                    : Math.Round(
                        incidents.Average(x =>
                            x.RiskScore),
                        2);

            return new()
            {
                From =
                    from,

                To =
                    to,

                TotalIncidents =
                    incidents.Count,

                OpenIncidents =
                    open,

                AcknowledgedIncidents =
                    acknowledged,

                ResolvedIncidents =
                    resolved.Count,

                HighIncidents =
                    high,

                CriticalIncidents =
                    critical,

                CriticalRate =
                    Percentage(
                        critical,
                        incidents.Count),

                ResolutionRate =
                    Percentage(
                        resolved.Count,
                        incidents.Count),

                MttrMinutes =
                    mttr,

                AverageRiskScore =
                    averageRisk,

                MaximumRiskScore =
                    maxRisk
            };
        }

        private static decimal Percentage(
            int value,
            int total)
        {
            if (total <= 0)
            {
                return 0m;
            }

            return Math.Round(
                (decimal)value /
                total *
                100m,
                2);
        }
    }

    public class PropertyMatchSalesAutomationRetryIncidentAnalyticsDto
    {
        public PropertyMatchSalesAutomationRetryIncidentWindowDto
            Last24Hours { get; set; } = new();

        public PropertyMatchSalesAutomationRetryIncidentWindowDto
            Last7Days { get; set; } = new();

        public PropertyMatchSalesAutomationRetryIncidentWindowDto
            Last30Days { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationRetryIncidentWindowDto
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int TotalIncidents { get; set; }

        public int OpenIncidents { get; set; }

        public int AcknowledgedIncidents { get; set; }

        public int ResolvedIncidents { get; set; }

        public int HighIncidents { get; set; }

        public int CriticalIncidents { get; set; }

        public decimal CriticalRate { get; set; }

        public decimal ResolutionRate { get; set; }

        public decimal MttrMinutes { get; set; }

        public decimal AverageRiskScore { get; set; }

        public decimal MaximumRiskScore { get; set; }
    }
}
