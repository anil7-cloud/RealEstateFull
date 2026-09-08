using REAL_ESTATE_CLEAN.Core.Domain.Entities;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryIncidentEscalationAnalyticsService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationRetryIncidentEscalationHistoryRepository
                _historyRepository;

        public PropertyCustomerMatchSalesAutomationRetryIncidentEscalationAnalyticsService(
            PropertyCustomerMatchSalesAutomationRetryIncidentEscalationHistoryRepository historyRepository)
        {
            _historyRepository =
                historyRepository;
        }

        public async Task<
            PropertyMatchSalesAutomationRetryIncidentEscalationAnalyticsDto>
            GetAnalyticsAsync(
                CancellationToken cancellationToken = default)
        {
            var now =
                DateTime.UtcNow;

            var history =
                await _historyRepository
                    .GetRecentAsync(
                        1000,
                        cancellationToken);

            return new()
            {
                Last24Hours =
                    BuildWindow(
                        history,
                        now.AddHours(-24),
                        now),

                Last7Days =
                    BuildWindow(
                        history,
                        now.AddDays(-7),
                        now),

                Last30Days =
                    BuildWindow(
                        history,
                        now.AddDays(-30),
                        now),

                GeneratedAt =
                    now
            };
        }

        private static
            PropertyMatchSalesAutomationRetryIncidentEscalationAnalyticsWindowDto
            BuildWindow(
                IEnumerable<
                    PropertyMatchSalesAutomationRetryIncidentEscalationHistory>
                    source,
                DateTime from,
                DateTime to)
        {
            var history =
                source
                    .Where(x =>
                        x.CreatedAt >= from &&
                        x.CreatedAt <= to)
                    .ToList();

            var created =
                history.Count(x =>
                    string.Equals(
                        x.Action,
                        "EscalationCreated",
                        StringComparison.OrdinalIgnoreCase));

            var raised =
                history.Count(x =>
                    string.Equals(
                        x.Action,
                        "EscalationRaised",
                        StringComparison.OrdinalIgnoreCase));

            var resolved =
                history.Count(x =>
                    string.Equals(
                        x.Action,
                        "EscalationResolved",
                        StringComparison.OrdinalIgnoreCase));

            var level1 =
                history.Count(x =>
                    x.NewLevelNumber == 1);

            var level2 =
                history.Count(x =>
                    x.NewLevelNumber == 2);

            var level3 =
                history.Count(x =>
                    x.NewLevelNumber == 3);

            var levelEvents =
                level1 +
                level2 +
                level3;

            var level3Rate =
                levelEvents == 0
                    ? 0m
                    : Math.Round(
                        (decimal)level3 /
                        levelEvents *
                        100m,
                        2);

            var averageOverdue =
                history.Count == 0
                    ? 0m
                    : Math.Round(
                        (decimal)history
                            .Average(x =>
                                x.OverdueMinutes),
                        2);

            var maximumOverdue =
                history.Count == 0
                    ? 0
                    : history.Max(x =>
                        x.OverdueMinutes);

            var mostCommonSeverity =
                history
                    .Where(x =>
                        !string.IsNullOrWhiteSpace(
                            x.Severity))
                    .GroupBy(x =>
                        x.Severity,
                        StringComparer.OrdinalIgnoreCase)
                    .OrderByDescending(x =>
                        x.Count())
                    .Select(x =>
                        x.Key)
                    .FirstOrDefault();

            return new()
            {
                From =
                    from,

                To =
                    to,

                TotalHistoryEvents =
                    history.Count,

                EscalationsCreated =
                    created,

                EscalationsRaised =
                    raised,

                EscalationsResolved =
                    resolved,

                Level1Count =
                    level1,

                Level2Count =
                    level2,

                Level3Count =
                    level3,

                Level3Rate =
                    level3Rate,

                AverageOverdueMinutes =
                    averageOverdue,

                MaximumOverdueMinutes =
                    maximumOverdue,

                MostCommonSeverity =
                    mostCommonSeverity
            };
        }
    }

    public class
        PropertyMatchSalesAutomationRetryIncidentEscalationAnalyticsDto
    {
        public
            PropertyMatchSalesAutomationRetryIncidentEscalationAnalyticsWindowDto
            Last24Hours { get; set; } = new();

        public
            PropertyMatchSalesAutomationRetryIncidentEscalationAnalyticsWindowDto
            Last7Days { get; set; } = new();

        public
            PropertyMatchSalesAutomationRetryIncidentEscalationAnalyticsWindowDto
            Last30Days { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class
        PropertyMatchSalesAutomationRetryIncidentEscalationAnalyticsWindowDto
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int TotalHistoryEvents { get; set; }

        public int EscalationsCreated { get; set; }

        public int EscalationsRaised { get; set; }

        public int EscalationsResolved { get; set; }

        public int Level1Count { get; set; }

        public int Level2Count { get; set; }

        public int Level3Count { get; set; }

        public decimal Level3Rate { get; set; }

        public decimal AverageOverdueMinutes { get; set; }

        public int MaximumOverdueMinutes { get; set; }

        public string? MostCommonSeverity { get; set; }
    }
}
