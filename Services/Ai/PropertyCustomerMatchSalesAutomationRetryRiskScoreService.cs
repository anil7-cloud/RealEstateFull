namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryRiskScoreService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationRetryHealthService
                _healthService;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryAlertAnalyticsService
                _analyticsService;

        public PropertyCustomerMatchSalesAutomationRetryRiskScoreService(
            PropertyCustomerMatchSalesAutomationRetryHealthService healthService,
            PropertyCustomerMatchSalesAutomationRetryAlertAnalyticsService analyticsService)
        {
            _healthService =
                healthService;

            _analyticsService =
                analyticsService;
        }

        public async Task<PropertyMatchSalesAutomationRetryRiskScoreDto>
            GetRiskScoreAsync(
                CancellationToken cancellationToken = default)
        {
            var health =
                await _healthService
                    .GetHealthAsync(
                        cancellationToken);

            var trend =
                await _analyticsService
                    .GetTrendAsync(
                        7,
                        cancellationToken);

            decimal riskScore = 0m;

            var factors =
                new List<PropertyMatchSalesAutomationRetryRiskFactorDto>();

            if (!health.DatabaseAvailable)
            {
                AddRisk(
                    factors,
                    "DatabaseUnavailable",
                    30m,
                    "Critical");

                riskScore += 30m;
            }

            if (!health.EventDatabaseAvailable)
            {
                AddRisk(
                    factors,
                    "EventDatabaseUnavailable",
                    20m,
                    "Critical");

                riskScore += 20m;
            }

            if (health.RetryFailureRate > 0m)
            {
                var failureRisk =
                    Math.Min(
                        20m,
                        health.RetryFailureRate * 0.20m);

                AddRisk(
                    factors,
                    "RetryFailureRate",
                    failureRisk,
                    failureRisk >= 15m
                        ? "Critical"
                        : "Warning");

                riskScore +=
                    failureRisk;
            }

            if (health.StaleClaims > 0)
            {
                var staleRisk =
                    Math.Min(
                        10m,
                        health.StaleClaims * 2m);

                AddRisk(
                    factors,
                    "StaleClaims",
                    staleRisk,
                    staleRisk >= 8m
                        ? "Critical"
                        : "Warning");

                riskScore +=
                    staleRisk;
            }

            if (health.LeaseLost > 0)
            {
                var leaseRisk =
                    Math.Min(
                        8m,
                        health.LeaseLost * 2m);

                AddRisk(
                    factors,
                    "LeaseLost",
                    leaseRisk,
                    "Warning");

                riskScore +=
                    leaseRisk;
            }

            if (health.ReadyForRetry > 0)
            {
                var backlogRisk =
                    Math.Min(
                        7m,
                        health.ReadyForRetry / 10m);

                if (backlogRisk > 0m)
                {
                    AddRisk(
                        factors,
                        "RetryBacklog",
                        backlogRisk,
                        backlogRisk >= 5m
                            ? "Critical"
                            : "Warning");

                    riskScore +=
                        backlogRisk;
                }
            }

            if (trend.Trend == "Worsening")
            {
                var trendRisk =
                    Math.Min(
                        10m,
                        Math.Max(
                            2m,
                            trend.AlertChangePercent / 10m));

                AddRisk(
                    factors,
                    "AlertTrendWorsening",
                    trendRisk,
                    trendRisk >= 7m
                        ? "Critical"
                        : "Warning");

                riskScore +=
                    trendRisk;
            }
            else if (trend.Trend == "Improving")
            {
                riskScore -=
                    3m;

                factors.Add(
                    new()
                    {
                        Code =
                            "AlertTrendImproving",

                        Impact =
                            -3m,

                        Severity =
                            "Positive"
                    });
            }

            riskScore =
                Math.Clamp(
                    Math.Round(
                        riskScore,
                        2),
                    0m,
                    100m);

            return new()
            {
                RiskScore =
                    riskScore,

                RiskLevel =
                    GetRiskLevel(
                        riskScore),

                OperationalHealthScore =
                    Math.Round(
                        100m - riskScore,
                        2),

                Trend =
                    trend.Trend,

                Factors =
                    factors
                        .OrderByDescending(x =>
                            x.Impact)
                        .ToList(),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static void AddRisk(
            List<PropertyMatchSalesAutomationRetryRiskFactorDto> factors,
            string code,
            decimal impact,
            string severity)
        {
            factors.Add(
                new()
                {
                    Code =
                        code,

                    Impact =
                        Math.Round(
                            impact,
                            2),

                    Severity =
                        severity
                });
        }

        private static string GetRiskLevel(
            decimal score)
        {
            if (score >= 75m)
                return "Critical";

            if (score >= 50m)
                return "High";

            if (score >= 25m)
                return "Moderate";

            if (score >= 10m)
                return "Low";

            return "Minimal";
        }
    }

    public class PropertyMatchSalesAutomationRetryRiskScoreDto
    {
        public decimal RiskScore { get; set; }

        public decimal OperationalHealthScore { get; set; }

        public string RiskLevel { get; set; }
            = string.Empty;

        public string Trend { get; set; }
            = string.Empty;

        public List<PropertyMatchSalesAutomationRetryRiskFactorDto>
            Factors { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationRetryRiskFactorDto
    {
        public string Code { get; set; }
            = string.Empty;

        public decimal Impact { get; set; }

        public string Severity { get; set; }
            = string.Empty;
    }
}
