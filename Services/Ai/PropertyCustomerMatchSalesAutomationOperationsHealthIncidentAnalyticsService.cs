namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsHealthIncidentAnalyticsService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsHealthIncidentRepository
                _repository;

        public PropertyCustomerMatchSalesAutomationOperationsHealthIncidentAnalyticsService(
            PropertyCustomerMatchSalesAutomationOperationsHealthIncidentRepository repository)
        {
            _repository =
                repository;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsHealthIncidentAnalyticsDto>
            GetAsync(
                CancellationToken cancellationToken = default)
        {
            var now =
                DateTime.UtcNow;

            var incidents =
                await _repository
                    .GetSinceAsync(
                        now.AddDays(-30),
                        10000,
                        cancellationToken);

            return new()
            {
                Last24Hours =
                    Build(
                        incidents,
                        now.AddHours(-24),
                        now),

                Last7Days =
                    Build(
                        incidents,
                        now.AddDays(-7),
                        now),

                Last30Days =
                    Build(
                        incidents,
                        now.AddDays(-30),
                        now),

                GeneratedAt =
                    now
            };
        }

        private static
            PropertyMatchSalesAutomationOperationsHealthIncidentAnalyticsWindowDto
            Build(
                IEnumerable<
                    Core.Domain.Entities.PropertyMatchSalesAutomationOperationsHealthIncident>
                    source,
                DateTime from,
                DateTime to)
        {
            var items =
                source
                    .Where(x =>
                        x.CreatedAt >= from &&
                        x.CreatedAt <= to)
                    .ToList();

            var resolved =
                items
                    .Where(x =>
                        x.ResolvedAt.HasValue)
                    .ToList();

            var averageResolutionMinutes =
                resolved.Count == 0
                    ? 0m
                    : Math.Round(
                        (decimal)resolved.Average(x =>
                            (x.ResolvedAt!.Value -
                             x.CreatedAt)
                            .TotalMinutes),
                        2);

            return new()
            {
                From =
                    from,

                To =
                    to,

                TotalIncidents =
                    items.Count,

                OpenIncidents =
                    items.Count(x =>
                        x.Status == "Open"),

                ResolvedIncidents =
                    items.Count(x =>
                        x.Status == "Resolved"),

                WarningIncidents =
                    items.Count(x =>
                        x.Severity == "Warning"),

                CriticalIncidents =
                    items.Count(x =>
                        x.Severity == "Critical"),

                AverageScoreDrop =
                    items.Count == 0
                        ? 0m
                        : Math.Round(
                            items.Average(x =>
                                x.ScoreDrop),
                            2),

                MaximumScoreDrop =
                    items.Count == 0
                        ? 0m
                        : items.Max(x =>
                            x.ScoreDrop),

                AverageResolutionMinutes =
                    averageResolutionMinutes
            };
        }
    }

    public class
        PropertyMatchSalesAutomationOperationsHealthIncidentAnalyticsDto
    {
        public PropertyMatchSalesAutomationOperationsHealthIncidentAnalyticsWindowDto
            Last24Hours { get; set; } = new();

        public PropertyMatchSalesAutomationOperationsHealthIncidentAnalyticsWindowDto
            Last7Days { get; set; } = new();

        public PropertyMatchSalesAutomationOperationsHealthIncidentAnalyticsWindowDto
            Last30Days { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class
        PropertyMatchSalesAutomationOperationsHealthIncidentAnalyticsWindowDto
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int TotalIncidents { get; set; }

        public int OpenIncidents { get; set; }

        public int ResolvedIncidents { get; set; }

        public int WarningIncidents { get; set; }

        public int CriticalIncidents { get; set; }

        public decimal AverageScoreDrop { get; set; }

        public decimal MaximumScoreDrop { get; set; }

        public decimal AverageResolutionMinutes { get; set; }
    }
}
