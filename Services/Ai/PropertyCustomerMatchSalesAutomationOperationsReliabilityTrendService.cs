using REAL_ESTATE_CLEAN.Core.Domain.Entities;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityTrendService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityHistoryRepository
                _historyRepository;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityTrendService(
            PropertyCustomerMatchSalesAutomationOperationsReliabilityHistoryRepository historyRepository)
        {
            _historyRepository =
                historyRepository;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsReliabilityTrendDto>
            GetTrendAsync(
                CancellationToken cancellationToken = default)
        {
            var now =
                DateTime.UtcNow;

            var history =
                await _historyRepository
                    .GetSinceAsync(
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

                GeneratedAt =
                    now
            };
        }

        private static
            PropertyMatchSalesAutomationOperationsReliabilityTrendWindowDto
            BuildWindow(
                IEnumerable<
                    PropertyMatchSalesAutomationOperationsReliabilityHistory>
                    source,
                DateTime from,
                DateTime to)
        {
            var items =
                source
                    .Where(x =>
                        x.CreatedAt >= from &&
                        x.CreatedAt <= to)
                    .OrderBy(x =>
                        x.CreatedAt)
                    .ToList();

            if (items.Count == 0)
            {
                return new()
                {
                    From = from,
                    To = to,
                    Trend = "NoData"
                };
            }

            var first =
                items.First();

            var latest =
                items.Last();

            var average =
                Math.Round(
                    items.Average(x =>
                        x.Score),
                    2);

            var minimum =
                items.Min(x =>
                    x.Score);

            var maximum =
                items.Max(x =>
                    x.Score);

            var change =
                Math.Round(
                    latest.Score -
                    first.Score,
                    2);

            var changePercent =
                first.Score == 0
                    ? 0m
                    : Math.Round(
                        change /
                        first.Score *
                        100m,
                        2);

            return new()
            {
                From =
                    from,

                To =
                    to,

                SnapshotCount =
                    items.Count,

                FirstScore =
                    first.Score,

                LatestScore =
                    latest.Score,

                AverageScore =
                    average,

                MinimumScore =
                    minimum,

                MaximumScore =
                    maximum,

                ScoreChange =
                    change,

                ScoreChangePercent =
                    changePercent,

                Trend =
                    GetTrend(
                        change),

                LatestGrade =
                    latest.Grade,

                LatestStatus =
                    latest.Status
            };
        }

        private static string GetTrend(
            decimal scoreChange)
        {
            if (scoreChange >= 2m)
            {
                return "Improving";
            }

            if (scoreChange <= -2m)
            {
                return "Degrading";
            }

            return "Stable";
        }
    }

    public class
        PropertyMatchSalesAutomationOperationsReliabilityTrendDto
    {
        public
            PropertyMatchSalesAutomationOperationsReliabilityTrendWindowDto
            Last24Hours { get; set; } = new();

        public
            PropertyMatchSalesAutomationOperationsReliabilityTrendWindowDto
            Last7Days { get; set; } = new();

        public
            PropertyMatchSalesAutomationOperationsReliabilityTrendWindowDto
            Last30Days { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class
        PropertyMatchSalesAutomationOperationsReliabilityTrendWindowDto
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int SnapshotCount { get; set; }

        public decimal FirstScore { get; set; }

        public decimal LatestScore { get; set; }

        public decimal AverageScore { get; set; }

        public decimal MinimumScore { get; set; }

        public decimal MaximumScore { get; set; }

        public decimal ScoreChange { get; set; }

        public decimal ScoreChangePercent { get; set; }

        public string Trend { get; set; }
            = "NoData";

        public string? LatestGrade { get; set; }

        public string? LatestStatus { get; set; }
    }
}
