namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsHealthTrendService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsHealthHistoryRepository
                _repository;

        public PropertyCustomerMatchSalesAutomationOperationsHealthTrendService(
            PropertyCustomerMatchSalesAutomationOperationsHealthHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsHealthTrendDto>
            GetAsync(
                CancellationToken cancellationToken = default)
        {
            var now =
                DateTime.UtcNow;

            var snapshots =
                await _repository
                    .GetSinceAsync(
                        now.AddDays(-30),
                        20000,
                        cancellationToken);

            return new()
            {
                Last24Hours =
                    Build(
                        snapshots,
                        now.AddHours(-24),
                        now),

                Last7Days =
                    Build(
                        snapshots,
                        now.AddDays(-7),
                        now),

                Last30Days =
                    Build(
                        snapshots,
                        now.AddDays(-30),
                        now),

                GeneratedAt =
                    now
            };
        }

        private static
            PropertyMatchSalesAutomationOperationsHealthTrendWindowDto
            Build(
                IEnumerable<
                    Core.Domain.Entities.PropertyMatchSalesAutomationOperationsHealthSnapshot>
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
                    To = to
                };
            }

            var first =
                items.First();

            var last =
                items.Last();

            var change =
                Math.Round(
                    last.Score -
                    first.Score,
                    2);

            return new()
            {
                From = from,
                To = to,

                SnapshotCount =
                    items.Count,

                FirstScore =
                    first.Score,

                LatestScore =
                    last.Score,

                MinimumScore =
                    items.Min(x =>
                        x.Score),

                MaximumScore =
                    items.Max(x =>
                        x.Score),

                AverageScore =
                    Math.Round(
                        items.Average(x =>
                            x.Score),
                        2),

                ScoreChange =
                    change,

                Direction =
                    change > 0m
                        ? "Improving"
                        : change < 0m
                            ? "Declining"
                            : "Stable"
            };
        }
    }

    public class
        PropertyMatchSalesAutomationOperationsHealthTrendDto
    {
        public PropertyMatchSalesAutomationOperationsHealthTrendWindowDto
            Last24Hours { get; set; } = new();

        public PropertyMatchSalesAutomationOperationsHealthTrendWindowDto
            Last7Days { get; set; } = new();

        public PropertyMatchSalesAutomationOperationsHealthTrendWindowDto
            Last30Days { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class
        PropertyMatchSalesAutomationOperationsHealthTrendWindowDto
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int SnapshotCount { get; set; }

        public decimal FirstScore { get; set; }

        public decimal LatestScore { get; set; }

        public decimal MinimumScore { get; set; }

        public decimal MaximumScore { get; set; }

        public decimal AverageScore { get; set; }

        public decimal ScoreChange { get; set; }

        public string Direction { get; set; }
            = "Stable";
    }
}
