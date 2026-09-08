namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesNextBestActionService
    {
        private readonly
            PropertyCustomerMatchSalesRecommendationService
                _recommendationService;

        public PropertyCustomerMatchSalesNextBestActionService(
            PropertyCustomerMatchSalesRecommendationService
                recommendationService)
        {
            _recommendationService = recommendationService;
        }

        public async Task<List<PropertyMatchNextBestActionDto>>
            GetActionsAsync(int limit = 50)
        {
            var recommendations =
                await _recommendationService
                    .GetRecommendationsAsync(100);

            return recommendations
                .Select(CreateAction)
                .OrderByDescending(x => x.ActionScore)
                .ThenByDescending(x => x.UrgencyScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<PropertyMatchNextBestActionDto?>
            GetForMatchAsync(int matchId)
        {
            if (matchId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(matchId));
            }

            var recommendation =
                await _recommendationService
                    .GetMatchRecommendationAsync(matchId);

            if (recommendation == null)
                return null;

            return CreateAction(recommendation);
        }

        public async Task<List<PropertyMatchNextBestActionDto>>
            GetForLeadAsync(
                int leadId,
                int limit = 20)
        {
            if (leadId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(leadId));
            }

            var recommendations =
                await _recommendationService
                    .GetLeadRecommendationsAsync(
                        leadId,
                        100);

            return recommendations
                .Select(CreateAction)
                .OrderByDescending(x => x.ActionScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<PropertyMatchNextBestActionDto?>
            GetBestForLeadAsync(int leadId)
        {
            var actions =
                await GetForLeadAsync(
                    leadId,
                    100);

            return actions
                .OrderByDescending(x => x.ActionScore)
                .ThenByDescending(x => x.UrgencyScore)
                .FirstOrDefault();
        }

        public async Task<List<PropertyMatchNextBestActionDto>>
            GetImmediateActionsAsync(int limit = 20)
        {
            var actions =
                await GetActionsAsync(100);

            return actions
                .Where(x => x.RequiresImmediateAction)
                .OrderByDescending(x => x.ActionScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<PropertyMatchNextBestActionSummaryDto>
            GetSummaryAsync()
        {
            var actions =
                await GetActionsAsync(100);

            if (actions.Count == 0)
            {
                return new PropertyMatchNextBestActionSummaryDto
                {
                    GeneratedAt = DateTime.UtcNow
                };
            }

            return new PropertyMatchNextBestActionSummaryDto
            {
                TotalActions =
                    actions.Count,

                ImmediateActions =
                    actions.Count(
                        x => x.RequiresImmediateAction),

                CriticalActions =
                    actions.Count(
                        x => x.Urgency == "Critical"),

                HighUrgencyActions =
                    actions.Count(
                        x => x.Urgency == "High"),

                CallActions =
                    actions.Count(
                        x => x.NextBestAction == "Call"),

                MeetingActions =
                    actions.Count(
                        x => x.NextBestAction ==
                             "ScheduleMeeting"),

                OfferActions =
                    actions.Count(
                        x => x.NextBestAction ==
                             "SendOffer"),

                FollowUpActions =
                    actions.Count(
                        x => x.NextBestAction ==
                             "FollowUp"),

                AlternativeActions =
                    actions.Count(
                        x => x.NextBestAction ==
                             "SuggestAlternative"),

                AverageActionScore =
                    Math.Round(
                        actions.Average(
                            x => x.ActionScore),
                        2),

                AverageUrgencyScore =
                    Math.Round(
                        actions.Average(
                            x => x.UrgencyScore),
                        2),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static PropertyMatchNextBestActionDto
            CreateAction(
                PropertyMatchSalesRecommendationDto recommendation)
        {
            var urgencyScore =
                CalculateUrgencyScore(
                    recommendation);

            var actionScore =
                CalculateActionScore(
                    recommendation,
                    urgencyScore);

            var urgency =
                GetUrgency(
                    urgencyScore);

            return new PropertyMatchNextBestActionDto
            {
                MatchId =
                    recommendation.MatchId,

                LeadId =
                    recommendation.LeadId,

                PropertyId =
                    recommendation.PropertyId,

                Stage =
                    recommendation.Stage,

                NextBestAction =
                    recommendation.Action,

                ActionTitle =
                    recommendation.ActionTitle,

                ConversionProbability =
                    recommendation.ConversionProbability,

                RiskScore =
                    recommendation.RiskScore,

                ForecastScore =
                    recommendation.ForecastScore,

                PriorityScore =
                    recommendation.PriorityScore,

                ActionScore =
                    Math.Round(
                        actionScore,
                        2),

                UrgencyScore =
                    Math.Round(
                        urgencyScore,
                        2),

                Urgency =
                    urgency,

                RecommendedChannel =
                    GetRecommendedChannel(
                        recommendation.Action),

                ExecuteWithinHours =
                    GetExecutionHours(
                        urgency,
                        recommendation.Action),

                Reason =
                    recommendation.Reason,

                Recommendation =
                    recommendation.Recommendation,

                RequiresImmediateAction =
                    recommendation.RequiresImmediateAction ||
                    urgencyScore >= 80,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static decimal CalculateUrgencyScore(
            PropertyMatchSalesRecommendationDto recommendation)
        {
            decimal score = 0;

            score +=
                recommendation.PriorityScore *
                0.40m;

            score +=
                recommendation.RiskScore *
                0.25m;

            score +=
                recommendation.ConversionProbability *
                0.20m;

            score +=
                GetStageUrgency(
                    recommendation.Stage) *
                0.15m;

            if (recommendation.RequiresImmediateAction)
            {
                score += 10;
            }

            return Math.Clamp(
                score,
                0,
                100);
        }

        private static decimal CalculateActionScore(
            PropertyMatchSalesRecommendationDto recommendation,
            decimal urgencyScore)
        {
            var actionWeight =
                GetActionWeight(
                    recommendation.Action);

            var score =
                (recommendation.PriorityScore * 0.30m) +
                (recommendation.ConversionProbability * 0.25m) +
                (recommendation.ForecastScore * 0.20m) +
                (urgencyScore * 0.15m) +
                (actionWeight * 0.10m);

            return Math.Clamp(
                score,
                0,
                100);
        }

        private static decimal GetStageUrgency(
            string stage)
        {
            if (string.IsNullOrWhiteSpace(stage))
                return 30;

            return stage
                .Trim()
                .ToLowerInvariant() switch
            {
                "offer" => 100,
                "meeting" => 85,
                "contacted" => 70,
                "viewed" => 50,
                "new" => 45,
                "lost" => 25,
                "won" => 0,
                _ => 30
            };
        }

        private static decimal GetActionWeight(
            string action)
        {
            return action switch
            {
                "SendOffer" => 100,
                "ScheduleMeeting" => 90,
                "Call" => 80,
                "FollowUp" => 70,
                "SuggestAlternative" => 55,
                "Wait" => 10,
                _ => 40
            };
        }

        private static string GetUrgency(
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

        private static string GetRecommendedChannel(
            string action)
        {
            return action switch
            {
                "Call" =>
                    "Phone",

                "ScheduleMeeting" =>
                    "PhoneOrEmail",

                "SendOffer" =>
                    "Email",

                "FollowUp" =>
                    "PhoneOrWhatsApp",

                "SuggestAlternative" =>
                    "EmailOrWhatsApp",

                "Wait" =>
                    "None",

                _ =>
                    "CRM"
            };
        }

        private static int GetExecutionHours(
            string urgency,
            string action)
        {
            if (action == "Wait")
                return 72;

            return urgency switch
            {
                "Critical" => 1,
                "High" => 4,
                "Medium" => 24,
                "Low" => 48,
                _ => 72
            };
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

    public class PropertyMatchNextBestActionDto
    {
        public int MatchId { get; set; }

        public int LeadId { get; set; }

        public int PropertyId { get; set; }

        public string Stage { get; set; }
            = string.Empty;

        public string NextBestAction { get; set; }
            = string.Empty;

        public string ActionTitle { get; set; }
            = string.Empty;

        public decimal ConversionProbability { get; set; }

        public decimal RiskScore { get; set; }

        public decimal ForecastScore { get; set; }

        public decimal PriorityScore { get; set; }

        public decimal ActionScore { get; set; }

        public decimal UrgencyScore { get; set; }

        public string Urgency { get; set; }
            = string.Empty;

        public string RecommendedChannel { get; set; }
            = string.Empty;

        public int ExecuteWithinHours { get; set; }

        public string Reason { get; set; }
            = string.Empty;

        public string Recommendation { get; set; }
            = string.Empty;

        public bool RequiresImmediateAction { get; set; }

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchNextBestActionSummaryDto
    {
        public int TotalActions { get; set; }

        public int ImmediateActions { get; set; }

        public int CriticalActions { get; set; }

        public int HighUrgencyActions { get; set; }

        public int CallActions { get; set; }

        public int MeetingActions { get; set; }

        public int OfferActions { get; set; }

        public int FollowUpActions { get; set; }

        public int AlternativeActions { get; set; }

        public decimal AverageActionScore { get; set; }

        public decimal AverageUrgencyScore { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
