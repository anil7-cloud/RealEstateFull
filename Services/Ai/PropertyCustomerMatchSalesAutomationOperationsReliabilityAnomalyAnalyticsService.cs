using REAL_ESTATE_CLEAN.Core.Domain.Entities;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityAnomalyAnalyticsService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAnomalyHistoryRepository
                _repository;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityAnomalyAnalyticsService(
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAnomalyHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsReliabilityAnomalyAnalyticsDto>
            GetAnalyticsAsync(
                CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            var history =
                await _repository.GetSinceAsync(
                    now.AddDays(-30),
                    10000,
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

                GeneratedAt = now
            };
        }

        private static
            PropertyMatchSalesAutomationOperationsReliabilityAnomalyAnalyticsWindowDto
            BuildWindow(
                IEnumerable<PropertyMatchSalesAutomationOperationsReliabilityAnomalyHistory> source,
                DateTime from,
                DateTime to)
        {
            var items =
                source
                    .Where(x =>
                        x.DetectedAt >= from &&
                        x.DetectedAt <= to)
                    .ToList();

            var warningCount =
                items.Count(x =>
                    string.Equals(
                        x.Severity,
                        "Warning",
                        StringComparison.OrdinalIgnoreCase));

            var criticalCount =
                items.Count(x =>
                    string.Equals(
                        x.Severity,
                        "Critical",
                        StringComparison.OrdinalIgnoreCase));

            var activeCount =
                items.Count(x =>
                    string.Equals(
                        x.Status,
                        "Active",
                        StringComparison.OrdinalIgnoreCase));

            var resolvedCount =
                items.Count(x =>
                    string.Equals(
                        x.Status,
                        "Resolved",
                        StringComparison.OrdinalIgnoreCase));

            var averageDrop =
                items.Count == 0
                    ? 0m
                    : Math.Round(
                        items.Average(x => x.ScoreDrop),
                        2);

            var maximumDrop =
                items.Count == 0
                    ? 0m
                    : items.Max(x => x.ScoreDrop);

            var mostCommonReason =
                items
                    .Where(x =>
                        !string.IsNullOrWhiteSpace(x.Reason))
                    .GroupBy(
                        x => x.Reason,
                        StringComparer.OrdinalIgnoreCase)
                    .OrderByDescending(x => x.Count())
                    .Select(x => x.Key)
                    .FirstOrDefault();

            return new()
            {
                From = from,
                To = to,
                TotalAnomalies = items.Count,
                WarningCount = warningCount,
                CriticalCount = criticalCount,
                ActiveCount = activeCount,
                ResolvedCount = resolvedCount,
                AverageScoreDrop = averageDrop,
                MaximumScoreDrop = maximumDrop,
                MostCommonReason = mostCommonReason
            };
        }
    }

    public class PropertyMatchSalesAutomationOperationsReliabilityAnomalyAnalyticsDto
    {
        public PropertyMatchSalesAutomationOperationsReliabilityAnomalyAnalyticsWindowDto
            Last24Hours { get; set; } = new();

        public PropertyMatchSalesAutomationOperationsReliabilityAnomalyAnalyticsWindowDto
            Last7Days { get; set; } = new();

        public PropertyMatchSalesAutomationOperationsReliabilityAnomalyAnalyticsWindowDto
            Last30Days { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationOperationsReliabilityAnomalyAnalyticsWindowDto
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int TotalAnomalies { get; set; }

        public int WarningCount { get; set; }

        public int CriticalCount { get; set; }

        public int ActiveCount { get; set; }

        public int ResolvedCount { get; set; }

        public decimal AverageScoreDrop { get; set; }

        public decimal MaximumScoreDrop { get; set; }

        public string? MostCommonReason { get; set; }
    }
}
