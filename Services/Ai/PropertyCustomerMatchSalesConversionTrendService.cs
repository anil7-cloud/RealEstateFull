namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesConversionTrendService
    {
        private readonly
            PropertyCustomerMatchSalesConversionPredictionService
                _predictionService;

        public PropertyCustomerMatchSalesConversionTrendService(
            PropertyCustomerMatchSalesConversionPredictionService
                predictionService)
        {
            _predictionService = predictionService;
        }

        public async Task<PropertyMatchConversionTrendDto>
            GetTrendAsync()
        {
            var predictions =
                await _predictionService.GetPredictionsAsync(100);

            if (predictions.Count == 0)
            {
                return new PropertyMatchConversionTrendDto
                {
                    Trend = "NoData",
                    Momentum = "NoData",
                    PipelineStrength = "NoData",
                    GeneratedAt = DateTime.UtcNow
                };
            }

            var averageProbability =
                predictions.Average(
                    x => x.ConversionProbability);

            var averageConfidence =
                predictions.Average(
                    x => x.Confidence);

            var highProbabilityCount =
                predictions.Count(
                    x => x.ConversionProbability >= 70);

            var mediumProbabilityCount =
                predictions.Count(
                    x =>
                        x.ConversionProbability >= 50 &&
                        x.ConversionProbability < 70);

            var lowProbabilityCount =
                predictions.Count(
                    x => x.ConversionProbability < 50);

            var predictedConversions =
                predictions.Count(
                    x => x.PredictedToConvert);

            var expectedConversions =
                predictions.Sum(
                    x => x.ConversionProbability) / 100m;

            var pipelineStrengthScore =
                CalculatePipelineStrength(
                    predictions);

            var momentumScore =
                CalculateMomentum(
                    predictions);

            return new PropertyMatchConversionTrendDto
            {
                TotalMatches =
                    predictions.Count,

                AverageConversionProbability =
                    Math.Round(
                        averageProbability,
                        2),

                AverageConfidence =
                    Math.Round(
                        averageConfidence,
                        2),

                HighProbabilityCount =
                    highProbabilityCount,

                MediumProbabilityCount =
                    mediumProbabilityCount,

                LowProbabilityCount =
                    lowProbabilityCount,

                PredictedConversions =
                    predictedConversions,

                ExpectedConversions =
                    Math.Round(
                        expectedConversions,
                        2),

                PipelineStrengthScore =
                    Math.Round(
                        pipelineStrengthScore,
                        2),

                PipelineStrength =
                    GetPipelineStrength(
                        pipelineStrengthScore),

                MomentumScore =
                    Math.Round(
                        momentumScore,
                        2),

                Momentum =
                    GetMomentum(
                        momentumScore),

                Trend =
                    GetTrend(
                        averageProbability,
                        momentumScore),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        public async Task<List<PropertyMatchConversionTrendItemDto>>
            GetTrendItemsAsync(int limit = 50)
        {
            limit = NormalizeLimit(limit);

            var predictions =
                await _predictionService
                    .GetPredictionsAsync(100);

            return predictions
                .Select(CreateTrendItem)
                .OrderByDescending(
                    x => x.MomentumScore)
                .ThenByDescending(
                    x => x.ConversionProbability)
                .Take(limit)
                .ToList();
        }

        public async Task<
            List<PropertyMatchConversionTrendItemDto>>
            GetPositiveMomentumAsync(int limit = 20)
        {
            var items =
                await GetTrendItemsAsync(100);

            return items
                .Where(x =>
                    x.MomentumScore >= 65)
                .OrderByDescending(
                    x => x.MomentumScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<
            List<PropertyMatchConversionTrendItemDto>>
            GetWeakMomentumAsync(int limit = 20)
        {
            var items =
                await GetTrendItemsAsync(100);

            return items
                .Where(x =>
                    x.MomentumScore < 45)
                .OrderBy(
                    x => x.MomentumScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        private static PropertyMatchConversionTrendItemDto
            CreateTrendItem(
                PropertyMatchConversionPredictionDto prediction)
        {
            var momentum =
                CalculateItemMomentum(prediction);

            return new PropertyMatchConversionTrendItemDto
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

                MomentumScore =
                    Math.Round(momentum, 2),

                Momentum =
                    GetMomentum(momentum),

                Direction =
                    GetDirection(momentum),

                RecommendedAction =
                    prediction.RecommendedAction
            };
        }

        private static decimal CalculatePipelineStrength(
            List<PropertyMatchConversionPredictionDto> predictions)
        {
            if (predictions.Count == 0)
                return 0;

            var probabilityScore =
                predictions.Average(
                    x => x.ConversionProbability);

            var highQualityRate =
                (decimal)predictions.Count(
                    x =>
                        x.ConversionProbability >= 70)
                /
                predictions.Count
                *
                100m;

            var confidenceScore =
                predictions.Average(
                    x => x.Confidence);

            var result =
                (probabilityScore * 0.50m) +
                (highQualityRate * 0.30m) +
                (confidenceScore * 0.20m);

            return Math.Clamp(
                result,
                0,
                100);
        }

        private static decimal CalculateMomentum(
            List<PropertyMatchConversionPredictionDto> predictions)
        {
            if (predictions.Count == 0)
                return 0;

            var itemScores =
                predictions
                    .Select(
                        CalculateItemMomentum)
                    .ToList();

            return Math.Clamp(
                itemScores.Average(),
                0,
                100);
        }

        private static decimal CalculateItemMomentum(
            PropertyMatchConversionPredictionDto prediction)
        {
            decimal stageScore =
                prediction.Stage
                    ?.Trim()
                    .ToLowerInvariant() switch
                {
                    "won" => 100,
                    "offer" => 90,
                    "meeting" => 80,
                    "contacted" => 65,
                    "viewed" => 50,
                    "new" => 35,
                    "lost" => 0,
                    _ => 30
                };

            var score =
                (prediction.ConversionProbability * 0.55m) +
                (prediction.Confidence * 0.20m) +
                (stageScore * 0.25m);

            return Math.Clamp(
                score,
                0,
                100);
        }

        private static string GetPipelineStrength(
            decimal score)
        {
            return score switch
            {
                >= 80 => "VeryStrong",
                >= 65 => "Strong",
                >= 50 => "Moderate",
                >= 35 => "Weak",
                _ => "VeryWeak"
            };
        }

        private static string GetMomentum(
            decimal score)
        {
            return score switch
            {
                >= 80 => "VeryHigh",
                >= 65 => "High",
                >= 50 => "Moderate",
                >= 35 => "Low",
                _ => "VeryLow"
            };
        }

        private static string GetDirection(
            decimal momentum)
        {
            return momentum switch
            {
                >= 70 => "Rising",
                >= 50 => "Stable",
                >= 35 => "Slowing",
                _ => "Declining"
            };
        }

        private static string GetTrend(
            decimal averageProbability,
            decimal momentum)
        {
            if (
                averageProbability >= 70 &&
                momentum >= 70)
            {
                return "StrongGrowth";
            }

            if (
                averageProbability >= 55 &&
                momentum >= 55)
            {
                return "Growing";
            }

            if (
                averageProbability >= 40 &&
                momentum >= 40)
            {
                return "Stable";
            }

            if (
                averageProbability >= 25 ||
                momentum >= 25)
            {
                return "Weakening";
            }

            return "Declining";
        }

        private static int NormalizeLimit(int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(
                limit,
                100);
        }
    }

    public class PropertyMatchConversionTrendDto
    {
        public int TotalMatches { get; set; }

        public decimal AverageConversionProbability { get; set; }

        public decimal AverageConfidence { get; set; }

        public int HighProbabilityCount { get; set; }

        public int MediumProbabilityCount { get; set; }

        public int LowProbabilityCount { get; set; }

        public int PredictedConversions { get; set; }

        public decimal ExpectedConversions { get; set; }

        public decimal PipelineStrengthScore { get; set; }

        public string PipelineStrength { get; set; }
            = string.Empty;

        public decimal MomentumScore { get; set; }

        public string Momentum { get; set; }
            = string.Empty;

        public string Trend { get; set; }
            = string.Empty;

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchConversionTrendItemDto
    {
        public int MatchId { get; set; }

        public int LeadId { get; set; }

        public int PropertyId { get; set; }

        public string Stage { get; set; }
            = string.Empty;

        public decimal MatchScore { get; set; }

        public decimal ConversionProbability { get; set; }

        public decimal Confidence { get; set; }

        public decimal MomentumScore { get; set; }

        public string Momentum { get; set; }
            = string.Empty;

        public string Direction { get; set; }
            = string.Empty;

        public string RecommendedAction { get; set; }
            = string.Empty;
    }
}
