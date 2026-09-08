namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsHealthScoreService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityScoreService
                _reliabilityScoreService;

        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAnomalyService
                _anomalyService;

        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityEscalationSummaryService
                _escalationSummaryService;

        public PropertyCustomerMatchSalesAutomationOperationsHealthScoreService(
            PropertyCustomerMatchSalesAutomationOperationsReliabilityScoreService reliabilityScoreService,
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAnomalyService anomalyService,
            PropertyCustomerMatchSalesAutomationOperationsReliabilityEscalationSummaryService escalationSummaryService)
        {
            _reliabilityScoreService =
                reliabilityScoreService;

            _anomalyService =
                anomalyService;

            _escalationSummaryService =
                escalationSummaryService;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsHealthScoreDto>
            GetAsync(
                CancellationToken cancellationToken = default)
        {
            var reliability =
                await _reliabilityScoreService
                    .GetScoreAsync(
                        cancellationToken);

            var anomaly =
                await _anomalyService
                    .DetectAsync(
                        cancellationToken);

            var escalation =
                await _escalationSummaryService
                    .GetAsync(
                        cancellationToken);

            /*
             * Reliability score temel puandır.
             */
            decimal score =
                reliability.Score;

            decimal anomalyPenalty = 0m;
            decimal escalationPenalty = 0m;

            /*
             * Anomaly penalty.
             */
            if (string.Equals(
                anomaly.Severity,
                "Critical",
                StringComparison.OrdinalIgnoreCase))
            {
                anomalyPenalty = 20m;
            }
            else if (string.Equals(
                anomaly.Severity,
                "Warning",
                StringComparison.OrdinalIgnoreCase))
            {
                anomalyPenalty = 10m;
            }

            /*
             * Escalation penalty.
             *
             * L1 = 2
             * L2 = 5
             * L3 = 10
             */
            escalationPenalty =
                (escalation.L1 * 2m) +
                (escalation.L2 * 5m) +
                (escalation.L3 * 10m);

            /*
             * Çok fazla escalation olması durumunda
             * tek başına bütün score'u sıfırlamasın.
             */
            escalationPenalty =
                Math.Min(
                    escalationPenalty,
                    40m);

            score -=
                anomalyPenalty;

            score -=
                escalationPenalty;

            score =
                Math.Clamp(
                    score,
                    0m,
                    100m);

            score =
                Math.Round(
                    score,
                    2);

            return new()
            {
                Score =
                    score,

                Status =
                    GetStatus(score),

                ReliabilityScore =
                    reliability.Score,

                AnomalyPenalty =
                    anomalyPenalty,

                EscalationPenalty =
                    escalationPenalty,

                HasAnomaly =
                    anomaly.HasAnomaly,

                AnomalySeverity =
                    anomaly.Severity,

                ActiveEscalations =
                    escalation.ActiveEscalations,

                HighestEscalationLevel =
                    escalation.HighestLevel,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static string GetStatus(
            decimal score)
        {
            if (score >= 90m)
            {
                return "Excellent";
            }

            if (score >= 75m)
            {
                return "Healthy";
            }

            if (score >= 60m)
            {
                return "Degraded";
            }

            if (score >= 40m)
            {
                return "Unhealthy";
            }

            return "Critical";
        }
    }

    public class
        PropertyMatchSalesAutomationOperationsHealthScoreDto
    {
        public decimal Score { get; set; }

        public string Status { get; set; }
            = string.Empty;

        public decimal ReliabilityScore { get; set; }

        public decimal AnomalyPenalty { get; set; }

        public decimal EscalationPenalty { get; set; }

        public bool HasAnomaly { get; set; }

        public string AnomalySeverity { get; set; }
            = string.Empty;

        public int ActiveEscalations { get; set; }

        public string HighestEscalationLevel { get; set; }
            = "None";

        public DateTime GeneratedAt { get; set; }
    }
}
