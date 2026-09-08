namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationAnalyticsService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationRepository
                _repository;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationAnalyticsService(
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationRepository repository)
        {
            _repository = repository;
        }

        public async Task<
            PropertyMatchSalesAutomationReliabilityAlertSlaEscalationAnalyticsDto>
            GetAnalyticsAsync(
                CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            var escalations =
                await _repository.GetSinceAsync(
                    now.AddDays(-30),
                    10000,
                    cancellationToken);

            return new()
            {
                Last24Hours = Build(
                    escalations,
                    now.AddHours(-24),
                    now),

                Last7Days = Build(
                    escalations,
                    now.AddDays(-7),
                    now),

                Last30Days = Build(
                    escalations,
                    now.AddDays(-30),
                    now),

                GeneratedAt = now
            };
        }

        private static
            PropertyMatchSalesAutomationReliabilityAlertSlaEscalationAnalyticsWindowDto
            Build(
                IEnumerable<
                    Core.Domain.Entities.PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalation>
                    source,
                DateTime from,
                DateTime to)
        {
            var items =
                source
                    .Where(x =>
                        x.EscalatedAt >= from &&
                        x.EscalatedAt <= to)
                    .ToList();

            var averageOverdue =
                items.Count == 0
                    ? 0m
                    : Math.Round(
                        items.Average(x =>
                            x.OverdueMinutes),
                        2);

            var maximumOverdue =
                items.Count == 0
                    ? 0m
                    : items.Max(x =>
                        x.OverdueMinutes);

            return new()
            {
                From = from,
                To = to,

                TotalEscalations =
                    items.Count,

                L1Count =
                    items.Count(x =>
                        x.EscalationLevel == "L1"),

                L2Count =
                    items.Count(x =>
                        x.EscalationLevel == "L2"),

                L3Count =
                    items.Count(x =>
                        x.EscalationLevel == "L3"),

                ActiveCount =
                    items.Count(x =>
                        x.Status == "Active"),

                ResolvedCount =
                    items.Count(x =>
                        x.Status == "Resolved"),

                P1Count =
                    items.Count(x =>
                        x.Priority == "P1"),

                P2Count =
                    items.Count(x =>
                        x.Priority == "P2"),

                AverageOverdueMinutes =
                    averageOverdue,

                MaximumOverdueMinutes =
                    maximumOverdue
            };
        }
    }

    public class
        PropertyMatchSalesAutomationReliabilityAlertSlaEscalationAnalyticsDto
    {
        public PropertyMatchSalesAutomationReliabilityAlertSlaEscalationAnalyticsWindowDto
            Last24Hours { get; set; } = new();

        public PropertyMatchSalesAutomationReliabilityAlertSlaEscalationAnalyticsWindowDto
            Last7Days { get; set; } = new();

        public PropertyMatchSalesAutomationReliabilityAlertSlaEscalationAnalyticsWindowDto
            Last30Days { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class
        PropertyMatchSalesAutomationReliabilityAlertSlaEscalationAnalyticsWindowDto
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int TotalEscalations { get; set; }

        public int L1Count { get; set; }

        public int L2Count { get; set; }

        public int L3Count { get; set; }

        public int ActiveCount { get; set; }

        public int ResolvedCount { get; set; }

        public int P1Count { get; set; }

        public int P2Count { get; set; }

        public decimal AverageOverdueMinutes { get; set; }

        public decimal MaximumOverdueMinutes { get; set; }
    }
}
