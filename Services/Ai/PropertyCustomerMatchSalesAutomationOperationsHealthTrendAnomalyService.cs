namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsHealthTrendAnomalyService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsHealthHistoryRepository
                _historyRepository;

        public PropertyCustomerMatchSalesAutomationOperationsHealthTrendAnomalyService(
            PropertyCustomerMatchSalesAutomationOperationsHealthHistoryRepository historyRepository)
        {
            _historyRepository =
                historyRepository;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsHealthTrendAnomalyDto>
            DetectAsync(
                CancellationToken cancellationToken = default)
        {
            var now =
                DateTime.UtcNow;

            /*
             * Son 24 saatteki health snapshot'larını inceliyoruz.
             */
            var snapshots =
                await _historyRepository
                    .GetSinceAsync(
                        now.AddHours(-24),
                        10000,
                        cancellationToken);

            var ordered =
                snapshots
                    .OrderBy(x => x.CreatedAt)
                    .ToList();

            if (ordered.Count < 2)
            {
                return new()
                {
                    HasAnomaly = false,
                    Severity = "None",
                    Reason = "InsufficientHistory",
                    SnapshotCount = ordered.Count,
                    GeneratedAt = now
                };
            }

            var first =
                ordered.First();

            var latest =
                ordered.Last();

            var scoreChange =
                Math.Round(
                    latest.Score - first.Score,
                    2);

            var scoreDrop =
                scoreChange < 0m
                    ? Math.Abs(scoreChange)
                    : 0m;

            string severity;
            bool hasAnomaly;

            /*
             * 20+ puan düşüş = Critical
             * 10+ puan düşüş = Warning
             */
            if (scoreDrop >= 20m)
            {
                severity =
                    "Critical";

                hasAnomaly =
                    true;
            }
            else if (scoreDrop >= 10m)
            {
                severity =
                    "Warning";

                hasAnomaly =
                    true;
            }
            else
            {
                severity =
                    "None";

                hasAnomaly =
                    false;
            }

            /*
             * Sadece başlangıç-son değerine değil,
             * son snapshot'ın son 24 saatteki zirveden
             * ne kadar düştüğüne de bak.
             */
            var peakScore =
                ordered.Max(x =>
                    x.Score);

            var dropFromPeak =
                Math.Round(
                    peakScore - latest.Score,
                    2);

            if (dropFromPeak >= 20m)
            {
                severity =
                    "Critical";

                hasAnomaly =
                    true;
            }
            else if (
                dropFromPeak >= 10m &&
                severity != "Critical")
            {
                severity =
                    "Warning";

                hasAnomaly =
                    true;
            }

            var effectiveDrop =
                Math.Max(
                    scoreDrop,
                    dropFromPeak);

            return new()
            {
                HasAnomaly =
                    hasAnomaly,

                Severity =
                    severity,

                Reason =
                    hasAnomaly
                        ? "HealthScoreDrop"
                        : "Stable",

                FirstScore =
                    first.Score,

                LatestScore =
                    latest.Score,

                PeakScore =
                    peakScore,

                ScoreChange =
                    scoreChange,

                ScoreDrop =
                    effectiveDrop,

                SnapshotCount =
                    ordered.Count,

                WindowStart =
                    now.AddHours(-24),

                WindowEnd =
                    now,

                GeneratedAt =
                    now
            };
        }
    }

    public class
        PropertyMatchSalesAutomationOperationsHealthTrendAnomalyDto
    {
        public bool HasAnomaly { get; set; }

        public string Severity { get; set; }
            = "None";

        public string Reason { get; set; }
            = string.Empty;

        public decimal FirstScore { get; set; }

        public decimal LatestScore { get; set; }

        public decimal PeakScore { get; set; }

        public decimal ScoreChange { get; set; }

        public decimal ScoreDrop { get; set; }

        public int SnapshotCount { get; set; }

        public DateTime WindowStart { get; set; }

        public DateTime WindowEnd { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
