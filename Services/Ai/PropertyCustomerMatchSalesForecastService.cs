namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesForecastService
    {
        private readonly
            PropertyCustomerMatchSalesConversionPredictionService
                _predictionService;

        public PropertyCustomerMatchSalesForecastService(
            PropertyCustomerMatchSalesConversionPredictionService
                predictionService)
        {
            _predictionService = predictionService;
        }

        public async Task<PropertyMatchSalesForecastDto>
            GetForecastAsync()
        {
            var predictions =
                await _predictionService
                    .GetPredictionsAsync(100);

            if (predictions.Count == 0)
            {
                return new PropertyMatchSalesForecastDto
                {
                    ForecastLevel = "NoData",
                    PipelineHealth = "NoData",
                    GeneratedAt = DateTime.UtcNow
                };
            }

            var active = predictions
                .Where(x =>
                    !IsStatus(x.Stage, "Won") &&
                    !IsStatus(x.Stage, "Lost"))
                .ToList();

            var wonCount = predictions.Count(
                x => IsStatus(x.Stage, "Won"));

            var lostCount = predictions.Count(
                x => IsStatus(x.Stage, "Lost"));

            var expectedActiveConversions =
                active.Sum(
                    x => x.ConversionProbability / 100m);

            var expectedTotalSales =
                wonCount +
                expectedActiveConversions;

            var highProbability =
                active.Count(
                    x => x.ConversionProbability >= 70);

            var veryHighProbability =
                active.Count(
                    x => x.ConversionProbability >= 85);

            var offerCount =
                active.Count(
                    x => IsStatus(x.Stage, "Offer"));

            var meetingCount =
                active.Count(
                    x => IsStatus(x.Stage, "Meeting"));

            var averageProbability =
                active.Count == 0
                    ? 0
                    : active.Average(
                        x => x.ConversionProbability);

            var averageConfidence =
                active.Count == 0
                    ? 0
                    : active.Average(
                        x => x.Confidence);

            var forecastScore =
                CalculateForecastScore(
                    active,
                    averageProbability,
                    averageConfidence);

            return new PropertyMatchSalesForecastDto
            {
                TotalMatches =
                    predictions.Count,

                ActiveOpportunities =
                    active.Count,

                WonSales =
                    wonCount,

                LostSales =
                    lostCount,

                OfferStage =
                    offerCount,

                MeetingStage =
                    meetingCount,

                HighProbabilityOpportunities =
                    highProbability,

                VeryHighProbabilityOpportunities =
                    veryHighProbability,

                AverageConversionProbability =
                    Math.Round(
                        averageProbability,
                        2),

                AverageConfidence =
                    Math.Round(
                        averageConfidence,
                        2),

                ExpectedActiveConversions =
                    Math.Round(
                        expectedActiveConversions,
                        2),

                ExpectedTotalSales =
                    Math.Round(
                        expectedTotalSales,
                        2),

                ForecastScore =
                    Math.Round(
                        forecastScore,
                        2),

                ForecastLevel =
                    GetForecastLevel(
                        forecastScore),

                PipelineHealth =
                    GetPipelineHealth(
                        active),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        public async Task<List<PropertyMatchSalesForecastItemDto>>
            GetForecastItemsAsync(int limit = 50)
        {
            limit = NormalizeLimit(limit);

            var predictions =
                await _predictionService
                    .GetPredictionsAsync(100);

            return predictions
                .Where(x =>
                    !IsStatus(x.Stage, "Won") &&
                    !IsStatus(x.Stage, "Lost"))
                .Select(CreateForecastItem)
                .OrderByDescending(
                    x => x.ForecastScore)
                .ThenByDescending(
                    x => x.ConversionProbability)
                .Take(limit)
                .ToList();
        }

        public async Task<List<PropertyMatchSalesForecastItemDto>>
            GetLikelySalesAsync(int limit = 20)
        {
            var items =
                await GetForecastItemsAsync(100);

            return items
                .Where(x =>
                    x.ConversionProbability >= 70)
                .OrderByDescending(
                    x => x.ForecastScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<List<PropertyMatchSalesForecastItemDto>>
            GetCriticalOpportunitiesAsync(
                int limit = 20)
        {
            var items =
                await GetForecastItemsAsync(100);

            return items
                .Where(x =>
                    x.ForecastLevel == "VeryHigh" ||
                    x.ForecastLevel == "High")
                .OrderByDescending(
                    x => x.ForecastScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<PropertyMatchSalesForecastStageDto>
            GetStageForecastAsync()
        {
            var predictions =
                await _predictionService
                    .GetPredictionsAsync(100);

            return new PropertyMatchSalesForecastStageDto
            {
                NewExpectedSales =
                    ExpectedForStage(
                        predictions,
                        "New"),

                ViewedExpectedSales =
                    ExpectedForStage(
                        predictions,
                        "Viewed"),

                ContactedExpectedSales =
                    ExpectedForStage(
                        predictions,
                        "Contacted"),

                MeetingExpectedSales =
                    ExpectedForStage(
                        predictions,
                        "Meeting"),

                OfferExpectedSales =
                    ExpectedForStage(
                        predictions,
                        "Offer"),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static PropertyMatchSalesForecastItemDto
            CreateForecastItem(
                PropertyMatchConversionPredictionDto prediction)
        {
            var stageWeight =
                GetStageWeight(
                    prediction.Stage);

            var forecastScore =
                (prediction.ConversionProbability * 0.65m) +
                (prediction.Confidence * 0.20m) +
                (stageWeight * 0.15m);

            forecastScore =
                Math.Clamp(
                    forecastScore,
                    0,
                    100);

            return new PropertyMatchSalesForecastItemDto
            {
                MatchId =
                    prediction.MatchId,

                LeadId =
                    prediction.LeadId,

                PropertyId =
                    prediction.PropertyId,

                Stage =
                    prediction.Stage,

                MatchScore =
                    prediction.MatchScore,

                ConversionProbability =
                    prediction.ConversionProbability,

                Confidence =
                    prediction.Confidence,

                ExpectedSaleValue =
                    Math.Round(
                        prediction.ConversionProbability /
                        100m,
                        4),

                ForecastScore =
                    Math.Round(
                        forecastScore,
                        2),

                ForecastLevel =
                    GetForecastLevel(
                        forecastScore),

                RecommendedAction =
                    prediction.RecommendedAction
            };
        }

        private static decimal CalculateForecastScore(
            List<PropertyMatchConversionPredictionDto> active,
            decimal averageProbability,
            decimal averageConfidence)
        {
            if (active.Count == 0)
                return 0;

            var highProbabilityRate =
                (decimal)active.Count(
                    x => x.ConversionProbability >= 70)
                /
                active.Count *
                100m;

            var advancedStageRate =
                (decimal)active.Count(
                    x =>
                        IsStatus(x.Stage, "Meeting") ||
                        IsStatus(x.Stage, "Offer"))
                /
                active.Count *
                100m;

            var score =
                (averageProbability * 0.45m) +
                (averageConfidence * 0.20m) +
                (highProbabilityRate * 0.20m) +
                (advancedStageRate * 0.15m);

            return Math.Clamp(
                score,
                0,
                100);
        }

        private static decimal ExpectedForStage(
            List<PropertyMatchConversionPredictionDto> predictions,
            string stage)
        {
            return Math.Round(
                predictions
                    .Where(x =>
                        IsStatus(
                            x.Stage,
                            stage))
                    .Sum(x =>
                        x.ConversionProbability /
                        100m),
                2);
        }

        private static decimal GetStageWeight(
            string stage)
        {
            if (string.IsNullOrWhiteSpace(stage))
                return 20;

            return stage
                .Trim()
                .ToLowerInvariant() switch
            {
                "offer" => 100,
                "meeting" => 85,
                "contacted" => 70,
                "viewed" => 50,
                "new" => 35,
                _ => 20
            };
        }

        private static string GetForecastLevel(
            decimal score)
        {
            return score switch
            {
                >= 85 => "VeryHigh",
                >= 70 => "High",
                >= 55 => "Medium",
                >= 40 => "Low",
                _ => "VeryLow"
            };
        }

        private static string GetPipelineHealth(
            List<PropertyMatchConversionPredictionDto> active)
        {
            if (active.Count == 0)
                return "Empty";

            var strongCount =
                active.Count(
                    x =>
                        x.ConversionProbability >= 70);

            var strongRate =
                (decimal)strongCount /
                active.Count *
                100m;

            return strongRate switch
            {
                >= 60 => "Excellent",
                >= 40 => "Strong",
                >= 25 => "Moderate",
                >= 10 => "Weak",
                _ => "Critical"
            };
        }

        private static bool IsStatus(
            string status,
            string expected)
        {
            return string.Equals(
                status,
                expected,
                StringComparison.OrdinalIgnoreCase);
        }

        private static int NormalizeLimit(
            int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(
                limit,
                100);
        }
    }

    public class PropertyMatchSalesForecastDto
    {
        public int TotalMatches { get; set; }

        public int ActiveOpportunities { get; set; }

        public int WonSales { get; set; }

        public int LostSales { get; set; }

        public int OfferStage { get; set; }

        public int MeetingStage { get; set; }

        public int HighProbabilityOpportunities { get; set; }

        public int VeryHighProbabilityOpportunities { get; set; }

        public decimal AverageConversionProbability { get; set; }

        public decimal AverageConfidence { get; set; }

        public decimal ExpectedActiveConversions { get; set; }

        public decimal ExpectedTotalSales { get; set; }

        public decimal ForecastScore { get; set; }

        public string ForecastLevel { get; set; }
            = string.Empty;

        public string PipelineHealth { get; set; }
            = string.Empty;

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesForecastItemDto
    {
        public int MatchId { get; set; }

        public int LeadId { get; set; }

        public int PropertyId { get; set; }

        public string Stage { get; set; }
            = string.Empty;

        public decimal MatchScore { get; set; }

        public decimal ConversionProbability { get; set; }

        public decimal Confidence { get; set; }

        public decimal ExpectedSaleValue { get; set; }

        public decimal ForecastScore { get; set; }

        public string ForecastLevel { get; set; }
            = string.Empty;

        public string RecommendedAction { get; set; }
            = string.Empty;
    }

    public class PropertyMatchSalesForecastStageDto
    {
        public decimal NewExpectedSales { get; set; }

        public decimal ViewedExpectedSales { get; set; }

        public decimal ContactedExpectedSales { get; set; }

        public decimal MeetingExpectedSales { get; set; }

        public decimal OfferExpectedSales { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
