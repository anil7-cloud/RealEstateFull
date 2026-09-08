namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesRecommendationService
    {
        private readonly
            PropertyCustomerMatchSalesConversionPredictionService
                _predictionService;

        private readonly
            PropertyCustomerMatchSalesRiskService
                _riskService;

        private readonly
            PropertyCustomerMatchSalesForecastService
                _forecastService;

        public PropertyCustomerMatchSalesRecommendationService(
            PropertyCustomerMatchSalesConversionPredictionService predictionService,
            PropertyCustomerMatchSalesRiskService riskService,
            PropertyCustomerMatchSalesForecastService forecastService)
        {
            _predictionService = predictionService;
            _riskService = riskService;
            _forecastService = forecastService;
        }

        public async Task<List<PropertyMatchSalesRecommendationDto>>
            GetRecommendationsAsync(int limit = 50)
        {
            limit = NormalizeLimit(limit);

            var predictionsTask =
                _predictionService.GetPredictionsAsync(100);

            var risksTask =
                _riskService.GetRisksAsync(100);

            var forecastTask =
                _forecastService.GetForecastItemsAsync(100);

            await Task.WhenAll(
                predictionsTask,
                risksTask,
                forecastTask);

            var predictions =
                await predictionsTask;

            var risks =
                await risksTask;

            var forecasts =
                await forecastTask;

            var riskDictionary = risks
                .GroupBy(x => x.MatchId)
                .ToDictionary(
                    x => x.Key,
                    x => x.First());

            var forecastDictionary = forecasts
                .GroupBy(x => x.MatchId)
                .ToDictionary(
                    x => x.Key,
                    x => x.First());

            var result =
                new List<PropertyMatchSalesRecommendationDto>();

            foreach (var prediction in predictions)
            {
                riskDictionary.TryGetValue(
                    prediction.MatchId,
                    out var risk);

                forecastDictionary.TryGetValue(
                    prediction.MatchId,
                    out var forecast);

                result.Add(
                    CreateRecommendation(
                        prediction,
                        risk,
                        forecast));
            }

            return result
                .OrderByDescending(x => x.PriorityScore)
                .ThenByDescending(x => x.ConversionProbability)
                .Take(limit)
                .ToList();
        }

        public async Task<PropertyMatchSalesRecommendationDto?>
            GetMatchRecommendationAsync(int matchId)
        {
            if (matchId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(matchId));
            }

            var predictionTask =
                _predictionService
                    .GetMatchPredictionAsync(matchId);

            var riskTask =
                _riskService
                    .GetMatchRiskAsync(matchId);

            await Task.WhenAll(
                predictionTask,
                riskTask);

            var prediction =
                await predictionTask;

            if (prediction == null)
                return null;

            var risk =
                await riskTask;

            var forecasts =
                await _forecastService
                    .GetForecastItemsAsync(100);

            var forecast = forecasts
                .FirstOrDefault(
                    x => x.MatchId == matchId);

            return CreateRecommendation(
                prediction,
                risk,
                forecast);
        }

        public async Task<List<PropertyMatchSalesRecommendationDto>>
            GetLeadRecommendationsAsync(
                int leadId,
                int limit = 20)
        {
            if (leadId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(leadId));
            }

            var recommendations =
                await GetRecommendationsAsync(100);

            return recommendations
                .Where(x => x.LeadId == leadId)
                .OrderByDescending(x => x.PriorityScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<List<PropertyMatchSalesRecommendationDto>>
            GetPropertyRecommendationsAsync(
                int propertyId,
                int limit = 20)
        {
            if (propertyId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(propertyId));
            }

            var recommendations =
                await GetRecommendationsAsync(100);

            return recommendations
                .Where(x => x.PropertyId == propertyId)
                .OrderByDescending(x => x.PriorityScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<List<PropertyMatchSalesRecommendationDto>>
            GetImmediateActionsAsync(int limit = 20)
        {
            var recommendations =
                await GetRecommendationsAsync(100);

            return recommendations
                .Where(x => x.RequiresImmediateAction)
                .OrderByDescending(x => x.PriorityScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<PropertyMatchSalesRecommendationSummaryDto>
            GetSummaryAsync()
        {
            var recommendations =
                await GetRecommendationsAsync(100);

            if (recommendations.Count == 0)
            {
                return new PropertyMatchSalesRecommendationSummaryDto
                {
                    GeneratedAt = DateTime.UtcNow
                };
            }

            return new PropertyMatchSalesRecommendationSummaryDto
            {
                TotalRecommendations =
                    recommendations.Count,

                ImmediateActions =
                    recommendations.Count(
                        x => x.RequiresImmediateAction),

                CallActions =
                    recommendations.Count(
                        x => x.Action == "Call"),

                MeetingActions =
                    recommendations.Count(
                        x => x.Action == "ScheduleMeeting"),

                OfferActions =
                    recommendations.Count(
                        x => x.Action == "SendOffer"),

                AlternativePropertyActions =
                    recommendations.Count(
                        x => x.Action == "SuggestAlternative"),

                FollowUpActions =
                    recommendations.Count(
                        x => x.Action == "FollowUp"),

                WaitActions =
                    recommendations.Count(
                        x => x.Action == "Wait"),

                AveragePriorityScore =
                    Math.Round(
                        recommendations.Average(
                            x => x.PriorityScore),
                        2),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static PropertyMatchSalesRecommendationDto
            CreateRecommendation(
                PropertyMatchConversionPredictionDto prediction,
                PropertyMatchSalesRiskDto? risk,
                PropertyMatchSalesForecastItemDto? forecast)
        {
            var riskScore =
                risk?.RiskScore ?? 0;

            var forecastScore =
                forecast?.ForecastScore ??
                prediction.ConversionProbability;

            var priorityScore =
                CalculatePriorityScore(
                    prediction,
                    riskScore,
                    forecastScore);

            var action =
                DetermineAction(
                    prediction,
                    risk);

            return new PropertyMatchSalesRecommendationDto
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

                RiskScore =
                    riskScore,

                ForecastScore =
                    forecastScore,

                PriorityScore =
                    Math.Round(
                        priorityScore,
                        2),

                Priority =
                    GetPriority(priorityScore),

                Action =
                    action,

                ActionTitle =
                    GetActionTitle(action),

                Recommendation =
                    BuildRecommendation(
                        action,
                        prediction,
                        risk),

                Reason =
                    BuildReason(
                        prediction,
                        risk),

                RequiresImmediateAction =
                    priorityScore >= 80 ||
                    riskScore >= 80 ||
                    IsStatus(
                        prediction.Stage,
                        "Offer"),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static decimal CalculatePriorityScore(
            PropertyMatchConversionPredictionDto prediction,
            decimal riskScore,
            decimal forecastScore)
        {
            decimal stageScore =
                prediction.Stage
                    ?.Trim()
                    .ToLowerInvariant() switch
                {
                    "offer" => 100,
                    "meeting" => 85,
                    "contacted" => 70,
                    "viewed" => 50,
                    "new" => 40,
                    "won" => 0,
                    "lost" => 10,
                    _ => 30
                };

            var score =
                (prediction.ConversionProbability * 0.40m) +
                (forecastScore * 0.25m) +
                (riskScore * 0.15m) +
                (stageScore * 0.20m);

            if (IsStatus(
                prediction.Stage,
                "Won"))
            {
                score = 0;
            }

            return Math.Clamp(
                score,
                0,
                100);
        }

        private static string DetermineAction(
            PropertyMatchConversionPredictionDto prediction,
            PropertyMatchSalesRiskDto? risk)
        {
            if (IsStatus(
                prediction.Stage,
                "Won"))
            {
                return "Wait";
            }

            if (IsStatus(
                prediction.Stage,
                "Lost"))
            {
                return "SuggestAlternative";
            }

            if (IsStatus(
                prediction.Stage,
                "Offer"))
            {
                return "FollowUp";
            }

            if (prediction.ConversionProbability >= 85)
            {
                return "SendOffer";
            }

            if (IsStatus(
                prediction.Stage,
                "Meeting"))
            {
                return "SendOffer";
            }

            if (prediction.ConversionProbability >= 70)
            {
                return "ScheduleMeeting";
            }

            if (risk != null &&
                risk.RiskScore >= 75)
            {
                return "Call";
            }

            if (prediction.MatchScore < 50)
            {
                return "SuggestAlternative";
            }

            if (prediction.ConversionProbability >= 45)
            {
                return "FollowUp";
            }

            if (prediction.ConversionProbability >= 25)
            {
                return "Call";
            }

            return "SuggestAlternative";
        }

        private static string BuildRecommendation(
            string action,
            PropertyMatchConversionPredictionDto prediction,
            PropertyMatchSalesRiskDto? risk)
        {
            return action switch
            {
                "SendOffer" =>
                    "Müşteriye uygun satış teklifini hazırla ve gönder.",

                "ScheduleMeeting" =>
                    "Müşteriyle satış görüşmesi veya ilan gösterimi planla.",

                "Call" =>
                    "Müşteriyi ara, ilgisini ve güncel ihtiyaçlarını doğrula.",

                "FollowUp" =>
                    "Müşteriyi takip et ve satış sürecinin bir sonraki adımını netleştir.",

                "SuggestAlternative" =>
                    "Müşterinin kriterlerine daha uygun alternatif ilanlar öner.",

                "Wait" =>
                    "Şu anda yeni satış aksiyonu gerekmiyor.",

                _ =>
                    prediction.RecommendedAction
            };
        }

        private static string BuildReason(
            PropertyMatchConversionPredictionDto prediction,
            PropertyMatchSalesRiskDto? risk)
        {
            if (risk != null &&
                risk.RiskScore >= 75)
            {
                return
                    $"Yüksek satış riski mevcut. Risk skoru: {risk.RiskScore:N2}. " +
                    risk.RiskReasons;
            }

            if (prediction.ConversionProbability >= 80)
            {
                return
                    $"Dönüşüm olasılığı yüksek: %{prediction.ConversionProbability:N2}.";
            }

            if (prediction.MatchScore < 50)
            {
                return
                    $"Müşteri-ilan eşleşme skoru düşük: {prediction.MatchScore:N2}.";
            }

            return
                $"Dönüşüm olasılığı %{prediction.ConversionProbability:N2}, " +
                $"eşleşme skoru {prediction.MatchScore:N2}.";
        }

        private static string GetActionTitle(
            string action)
        {
            return action switch
            {
                "Call" =>
                    "Müşteriyi Ara",

                "ScheduleMeeting" =>
                    "Toplantı Planla",

                "SendOffer" =>
                    "Teklif Gönder",

                "SuggestAlternative" =>
                    "Alternatif İlan Öner",

                "FollowUp" =>
                    "Takip Et",

                "Wait" =>
                    "Bekle",

                _ =>
                    "Satış Aksiyonu"
            };
        }

        private static string GetPriority(
            decimal score)
        {
            return score switch
            {
                >= 85 => "Critical",
                >= 70 => "High",
                >= 50 => "Medium",
                >= 30 => "Low",
                _ => "VeryLow"
            };
        }

        private static bool IsStatus(
            string? status,
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

    public class PropertyMatchSalesRecommendationDto
    {
        public int MatchId { get; set; }

        public int LeadId { get; set; }

        public int PropertyId { get; set; }

        public string Stage { get; set; }
            = string.Empty;

        public decimal MatchScore { get; set; }

        public decimal ConversionProbability { get; set; }

        public decimal RiskScore { get; set; }

        public decimal ForecastScore { get; set; }

        public decimal PriorityScore { get; set; }

        public string Priority { get; set; }
            = string.Empty;

        public string Action { get; set; }
            = string.Empty;

        public string ActionTitle { get; set; }
            = string.Empty;

        public string Recommendation { get; set; }
            = string.Empty;

        public string Reason { get; set; }
            = string.Empty;

        public bool RequiresImmediateAction { get; set; }

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesRecommendationSummaryDto
    {
        public int TotalRecommendations { get; set; }

        public int ImmediateActions { get; set; }

        public int CallActions { get; set; }

        public int MeetingActions { get; set; }

        public int OfferActions { get; set; }

        public int AlternativePropertyActions { get; set; }

        public int FollowUpActions { get; set; }

        public int WaitActions { get; set; }

        public decimal AveragePriorityScore { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
