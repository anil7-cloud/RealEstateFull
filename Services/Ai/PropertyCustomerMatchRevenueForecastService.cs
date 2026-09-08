using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchRevenueForecastService
    {
        private readonly IPropertyCustomerMatchService _matchService;

        public PropertyCustomerMatchRevenueForecastService(
            IPropertyCustomerMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<List<PropertyMatchRevenueForecastDto>>
            GetForecastsAsync(
                decimal defaultPropertyValue = 1_000_000m,
                decimal commissionRate = 2m,
                int limit = 50)
        {
            if (defaultPropertyValue < 0)
                defaultPropertyValue = 0;

            commissionRate = Math.Clamp(
                commissionRate,
                0,
                100);

            limit = NormalizeLimit(limit);

            var matches = await _matchService.GetAllAsync();

            return matches
                .Where(x => !IsClosedLost(x.Status))
                .Select(x => CreateForecast(
                    x,
                    defaultPropertyValue,
                    commissionRate))
                .OrderByDescending(x =>
                    x.ExpectedCommissionRevenue)
                .ThenByDescending(x =>
                    x.ConversionProbability)
                .Take(limit)
                .ToList();
        }

        public async Task<PropertyMatchRevenueForecastDto?>
            GetMatchForecastAsync(
                int matchId,
                decimal propertyValue,
                decimal commissionRate = 2m)
        {
            if (matchId <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(matchId));

            if (propertyValue < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(propertyValue));

            commissionRate = Math.Clamp(
                commissionRate,
                0,
                100);

            var match = await _matchService
                .GetByIdAsync(matchId);

            if (match == null)
                return null;

            return CreateForecast(
                match,
                propertyValue,
                commissionRate);
        }

        public async Task<PropertyMatchRevenueSummaryDto>
            GetSummaryAsync(
                decimal defaultPropertyValue = 1_000_000m,
                decimal commissionRate = 2m)
        {
            var forecasts = await GetForecastsAsync(
                defaultPropertyValue,
                commissionRate,
                100);

            var totalPotentialSalesValue =
                forecasts.Sum(x => x.PropertyValue);

            var totalExpectedSalesValue =
                forecasts.Sum(x => x.ExpectedSalesValue);

            var totalPotentialCommission =
                forecasts.Sum(x => x.PotentialCommission);

            var totalExpectedCommission =
                forecasts.Sum(x => x.ExpectedCommissionRevenue);

            var averageProbability =
                forecasts.Count == 0
                    ? 0
                    : forecasts.Average(
                        x => x.ConversionProbability);

            return new PropertyMatchRevenueSummaryDto
            {
                TotalOpportunities = forecasts.Count,

                TotalPotentialSalesValue =
                    Math.Round(
                        totalPotentialSalesValue,
                        2),

                TotalExpectedSalesValue =
                    Math.Round(
                        totalExpectedSalesValue,
                        2),

                TotalPotentialCommission =
                    Math.Round(
                        totalPotentialCommission,
                        2),

                TotalExpectedCommissionRevenue =
                    Math.Round(
                        totalExpectedCommission,
                        2),

                AverageConversionProbability =
                    Math.Round(
                        averageProbability,
                        2),

                GeneratedAt = DateTime.UtcNow
            };
        }

        private static PropertyMatchRevenueForecastDto
            CreateForecast(
                PropertyCustomerMatchDto match,
                decimal propertyValue,
                decimal commissionRate)
        {
            var probability =
                CalculateConversionProbability(match);

            var expectedSalesValue =
                propertyValue *
                probability /
                100m;

            var potentialCommission =
                propertyValue *
                commissionRate /
                100m;

            var expectedCommission =
                potentialCommission *
                probability /
                100m;

            return new PropertyMatchRevenueForecastDto
            {
                MatchId = match.Id,

                PropertyId = match.PropertyId,

                LeadId = match.LeadId,

                MatchScore = match.MatchScore,

                Stage = match.Status,

                PropertyValue =
                    Math.Round(propertyValue, 2),

                CommissionRate =
                    commissionRate,

                ConversionProbability =
                    probability,

                ExpectedSalesValue =
                    Math.Round(
                        expectedSalesValue,
                        2),

                PotentialCommission =
                    Math.Round(
                        potentialCommission,
                        2),

                ExpectedCommissionRevenue =
                    Math.Round(
                        expectedCommission,
                        2),

                ForecastLevel =
                    GetForecastLevel(probability),

                CalculatedAt =
                    DateTime.UtcNow
            };
        }

        private static decimal
            CalculateConversionProbability(
                PropertyCustomerMatchDto match)
        {
            var matchComponent =
                match.MatchScore * 0.60m;

            var stageComponent =
                GetStageProbabilityComponent(
                    match.Status);

            var criteriaComponent = 0m;

            if (match.PriceMatched)
                criteriaComponent += 5;

            if (match.LocationMatched)
                criteriaComponent += 5;

            if (match.PropertyTypeMatched)
                criteriaComponent += 3;

            if (match.RoomCountMatched)
                criteriaComponent += 3;

            if (match.SizeMatched)
                criteriaComponent += 4;

            var probability =
                matchComponent +
                stageComponent +
                criteriaComponent;

            if (string.Equals(
                    match.Status,
                    "Won",
                    StringComparison.OrdinalIgnoreCase))
            {
                return 100;
            }

            if (string.Equals(
                    match.Status,
                    "Lost",
                    StringComparison.OrdinalIgnoreCase))
            {
                return 0;
            }

            return Math.Round(
                Math.Clamp(
                    probability,
                    0,
                    95),
                2);
        }

        private static decimal
            GetStageProbabilityComponent(
                string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return 0;

            return status
                .Trim()
                .ToLowerInvariant() switch
            {
                "offer" => 20,
                "meeting" => 15,
                "contacted" => 10,
                "viewed" => 5,
                "new" => 2,
                _ => 0
            };
        }

        private static string GetForecastLevel(
            decimal probability)
        {
            return probability switch
            {
                >= 80 => "VeryHigh",
                >= 65 => "High",
                >= 45 => "Medium",
                >= 25 => "Low",
                _ => "VeryLow"
            };
        }

        private static bool IsClosedLost(
            string status)
        {
            return string.Equals(
                status,
                "Lost",
                StringComparison.OrdinalIgnoreCase);
        }

        private static int NormalizeLimit(int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(limit, 100);
        }
    }

    public class PropertyMatchRevenueForecastDto
    {
        public int MatchId { get; set; }

        public int PropertyId { get; set; }

        public int LeadId { get; set; }

        public decimal MatchScore { get; set; }

        public string Stage { get; set; }
            = string.Empty;

        public decimal PropertyValue { get; set; }

        public decimal CommissionRate { get; set; }

        public decimal ConversionProbability { get; set; }

        public decimal ExpectedSalesValue { get; set; }

        public decimal PotentialCommission { get; set; }

        public decimal ExpectedCommissionRevenue { get; set; }

        public string ForecastLevel { get; set; }
            = string.Empty;

        public DateTime CalculatedAt { get; set; }
    }

    public class PropertyMatchRevenueSummaryDto
    {
        public int TotalOpportunities { get; set; }

        public decimal TotalPotentialSalesValue { get; set; }

        public decimal TotalExpectedSalesValue { get; set; }

        public decimal TotalPotentialCommission { get; set; }

        public decimal TotalExpectedCommissionRevenue { get; set; }

        public decimal AverageConversionProbability { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
