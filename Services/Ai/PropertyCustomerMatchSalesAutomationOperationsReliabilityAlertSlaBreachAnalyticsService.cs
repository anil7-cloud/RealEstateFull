namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachAnalyticsService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachRepository
                _repository;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachAnalyticsService(
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachRepository repository)
        {
            _repository = repository;
        }

        public async Task<
            PropertyMatchSalesAutomationReliabilityAlertSlaBreachAnalyticsDto>
            GetAnalyticsAsync(
                CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            var breaches =
                await _repository.GetSinceAsync(
                    now.AddDays(-30),
                    10000,
                    cancellationToken);

            return new()
            {
                Last24Hours = Build(
                    breaches,
                    now.AddHours(-24),
                    now),

                Last7Days = Build(
                    breaches,
                    now.AddDays(-7),
                    now),

                Last30Days = Build(
                    breaches,
                    now.AddDays(-30),
                    now),

                GeneratedAt = now
            };
        }

        private static
            PropertyMatchSalesAutomationReliabilityAlertSlaBreachAnalyticsWindowDto
            Build(
                IEnumerable<
                    Core.Domain.Entities.PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreach>
                    source,
                DateTime from,
                DateTime to)
        {
            var items = source
                .Where(x =>
                    x.DetectedAt >= from &&
                    x.DetectedAt <= to)
                .ToList();

            var averageOverdue =
                items.Count == 0
                    ? 0m
                    : Math.Round(
                        items.Average(x => x.OverdueMinutes),
                        2);

            var maximumOverdue =
                items.Count == 0
                    ? 0m
                    : items.Max(x => x.OverdueMinutes);

            return new()
            {
                From = from,
                To = to,

                TotalBreaches =
                    items.Count,

                ActiveBreaches =
                    items.Count(x =>
                        x.Status == "Active"),

                ResolvedBreaches =
                    items.Count(x =>
                        x.Status == "Resolved"),

                P1Breaches =
                    items.Count(x =>
                        x.Priority == "P1"),

                P2Breaches =
                    items.Count(x =>
                        x.Priority == "P2"),

                AcknowledgeBreaches =
                    items.Count(x =>
                        x.BreachType == "Acknowledge"),

                ResolutionBreaches =
                    items.Count(x =>
                        x.BreachType == "Resolution"),

                AverageOverdueMinutes =
                    averageOverdue,

                MaximumOverdueMinutes =
                    maximumOverdue
            };
        }
    }

    public class
        PropertyMatchSalesAutomationReliabilityAlertSlaBreachAnalyticsDto
    {
        public PropertyMatchSalesAutomationReliabilityAlertSlaBreachAnalyticsWindowDto
            Last24Hours { get; set; } = new();

        public PropertyMatchSalesAutomationReliabilityAlertSlaBreachAnalyticsWindowDto
            Last7Days { get; set; } = new();

        public PropertyMatchSalesAutomationReliabilityAlertSlaBreachAnalyticsWindowDto
            Last30Days { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class
        PropertyMatchSalesAutomationReliabilityAlertSlaBreachAnalyticsWindowDto
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int TotalBreaches { get; set; }

        public int ActiveBreaches { get; set; }

        public int ResolvedBreaches { get; set; }

        public int P1Breaches { get; set; }

        public int P2Breaches { get; set; }

        public int AcknowledgeBreaches { get; set; }

        public int ResolutionBreaches { get; set; }

        public decimal AverageOverdueMinutes { get; set; }

        public decimal MaximumOverdueMinutes { get; set; }
    }
}
