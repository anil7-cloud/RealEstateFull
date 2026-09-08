namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationDecisionService
    {
        private readonly
            PropertyCustomerMatchSalesNextBestActionService
                _nextBestActionService;

        private readonly
            PropertyCustomerMatchSalesAutomationAnalyticsService
                _analyticsService;

        private readonly
            PropertyCustomerMatchSalesAutomationOptimizationService
                _optimizationService;

        public PropertyCustomerMatchSalesAutomationDecisionService(
            PropertyCustomerMatchSalesNextBestActionService
                nextBestActionService,
            PropertyCustomerMatchSalesAutomationAnalyticsService
                analyticsService,
            PropertyCustomerMatchSalesAutomationOptimizationService
                optimizationService)
        {
            _nextBestActionService =
                nextBestActionService;

            _analyticsService =
                analyticsService;

            _optimizationService =
                optimizationService;
        }

        public async Task<
            List<PropertyMatchSalesAutomationDecisionDto>>
            GetDecisionsAsync(int limit = 50)
        {
            var actionsTask =
                _nextBestActionService
                    .GetActionsAsync(100);

            var analyticsTask =
                _analyticsService
                    .GetAnalyticsAsync();

            var optimizationTask =
                _optimizationService
                    .GetOptimizationAsync();

            await Task.WhenAll(
                actionsTask,
                analyticsTask,
                optimizationTask);

            var actions =
                await actionsTask;

            var analytics =
                await analyticsTask;

            var optimization =
                await optimizationTask;

            return actions
                .Select(action =>
                    CreateDecision(
                        action,
                        analytics,
                        optimization))
                .OrderByDescending(
                    x => x.DecisionScore)
                .ThenByDescending(
                    x => x.ConfidenceScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<
            PropertyMatchSalesAutomationDecisionDto?>
            GetForMatchAsync(int matchId)
        {
            if (matchId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(matchId));
            }

            var actionTask =
                _nextBestActionService
                    .GetForMatchAsync(matchId);

            var analyticsTask =
                _analyticsService
                    .GetAnalyticsAsync();

            var optimizationTask =
                _optimizationService
                    .GetOptimizationAsync();

            await Task.WhenAll(
                actionTask,
                analyticsTask,
                optimizationTask);

            var action =
                await actionTask;

            if (action == null)
                return null;

            return CreateDecision(
                action,
                await analyticsTask,
                await optimizationTask);
        }

        public async Task<
            List<PropertyMatchSalesAutomationDecisionDto>>
            GetForLeadAsync(
                int leadId,
                int limit = 20)
        {
            if (leadId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(leadId));
            }

            var actionsTask =
                _nextBestActionService
                    .GetForLeadAsync(
                        leadId,
                        100);

            var analyticsTask =
                _analyticsService
                    .GetAnalyticsAsync();

            var optimizationTask =
                _optimizationService
                    .GetOptimizationAsync();

            await Task.WhenAll(
                actionsTask,
                analyticsTask,
                optimizationTask);

            var actions =
                await actionsTask;

            var analytics =
                await analyticsTask;

            var optimization =
                await optimizationTask;

            return actions
                .Select(action =>
                    CreateDecision(
                        action,
                        analytics,
                        optimization))
                .OrderByDescending(
                    x => x.DecisionScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<
            PropertyMatchSalesAutomationDecisionDto?>
            GetBestDecisionAsync()
        {
            var decisions =
                await GetDecisionsAsync(1);

            return decisions.FirstOrDefault();
        }

        public async Task<
            List<PropertyMatchSalesAutomationDecisionDto>>
            GetAutoExecuteDecisionsAsync(
                int limit = 20)
        {
            var decisions =
                await GetDecisionsAsync(100);

            return decisions
                .Where(x =>
                    x.Decision ==
                    "AutoExecute")
                .OrderByDescending(
                    x => x.DecisionScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<
            List<PropertyMatchSalesAutomationDecisionDto>>
            GetApprovalRequiredDecisionsAsync(
                int limit = 20)
        {
            var decisions =
                await GetDecisionsAsync(100);

            return decisions
                .Where(x =>
                    x.Decision ==
                    "RequireApproval")
                .OrderByDescending(
                    x => x.DecisionScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<
            List<PropertyMatchSalesAutomationDecisionDto>>
            GetHoldDecisionsAsync(
                int limit = 20)
        {
            var decisions =
                await GetDecisionsAsync(100);

            return decisions
                .Where(x =>
                    x.Decision == "Hold")
                .OrderByDescending(
                    x => x.DecisionScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<
            PropertyMatchSalesAutomationDecisionSummaryDto>
            GetSummaryAsync()
        {
            var decisions =
                await GetDecisionsAsync(100);

            if (decisions.Count == 0)
            {
                return new
                    PropertyMatchSalesAutomationDecisionSummaryDto
                {
                    GeneratedAt =
                        DateTime.UtcNow
                };
            }

            return new
                PropertyMatchSalesAutomationDecisionSummaryDto
            {
                TotalDecisions =
                    decisions.Count,

                AutoExecuteDecisions =
                    decisions.Count(
                        x =>
                            x.Decision ==
                            "AutoExecute"),

                ApprovalRequiredDecisions =
                    decisions.Count(
                        x =>
                            x.Decision ==
                            "RequireApproval"),

                HoldDecisions =
                    decisions.Count(
                        x =>
                            x.Decision ==
                            "Hold"),

                RejectDecisions =
                    decisions.Count(
                        x =>
                            x.Decision ==
                            "Reject"),

                CriticalDecisions =
                    decisions.Count(
                        x =>
                            x.Priority ==
                            "Critical"),

                HighConfidenceDecisions =
                    decisions.Count(
                        x =>
                            x.ConfidenceScore >= 80),

                AverageDecisionScore =
                    Math.Round(
                        decisions.Average(
                            x =>
                                x.DecisionScore),
                        2),

                AverageConfidenceScore =
                    Math.Round(
                        decisions.Average(
                            x =>
                                x.ConfidenceScore),
                        2),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static
            PropertyMatchSalesAutomationDecisionDto
            CreateDecision(
                PropertyMatchNextBestActionDto action,
                PropertyMatchSalesAutomationAnalyticsDto analytics,
                PropertyMatchSalesAutomationOptimizationDto optimization)
        {
            var historicalSuccess =
                GetHistoricalActionSuccess(
                    action.NextBestAction,
                    analytics);

            var optimizationPenalty =
                GetOptimizationPenalty(
                    action.NextBestAction,
                    optimization);

            var confidence =
                CalculateConfidence(
                    action,
                    analytics,
                    historicalSuccess,
                    optimizationPenalty);

            var decisionScore =
                CalculateDecisionScore(
                    action,
                    analytics,
                    confidence,
                    historicalSuccess,
                    optimizationPenalty);

            var decision =
                GetDecision(
                    action,
                    decisionScore,
                    confidence,
                    optimizationPenalty);

            var priority =
                GetPriority(
                    decisionScore,
                    action.Urgency);

            return new
                PropertyMatchSalesAutomationDecisionDto
            {
                MatchId =
                    action.MatchId,

                LeadId =
                    action.LeadId,

                PropertyId =
                    action.PropertyId,

                Stage =
                    action.Stage,

                RecommendedAction =
                    action.NextBestAction,

                ActionTitle =
                    action.ActionTitle,

                RecommendedChannel =
                    action.RecommendedChannel,

                Decision =
                    decision,

                DecisionScore =
                    Math.Round(
                        decisionScore,
                        2),

                ConfidenceScore =
                    Math.Round(
                        confidence,
                        2),

                HistoricalSuccessRate =
                    historicalSuccess,

                OptimizationPenalty =
                    optimizationPenalty,

                Priority =
                    priority,

                Urgency =
                    action.Urgency,

                UrgencyScore =
                    action.UrgencyScore,

                ConversionProbability =
                    action.ConversionProbability,

                RiskScore =
                    action.RiskScore,

                ExecuteWithinHours =
                    action.ExecuteWithinHours,

                RequiresHumanApproval =
                    decision ==
                    "RequireApproval",

                CanAutoExecute =
                    decision ==
                    "AutoExecute",

                Reason =
                    BuildReason(
                        action,
                        analytics,
                        decision,
                        confidence,
                        historicalSuccess,
                        optimizationPenalty),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static decimal
            GetHistoricalActionSuccess(
                string action,
                PropertyMatchSalesAutomationAnalyticsDto analytics)
        {
            var performance =
                analytics.ActionPerformance
                    .FirstOrDefault(x =>
                        string.Equals(
                            x.Action,
                            action,
                            StringComparison.OrdinalIgnoreCase));

            if (performance == null)
            {
                return analytics.TotalExecutions > 0
                    ? analytics.SuccessRate
                    : 50m;
            }

            return performance.SuccessRate;
        }

        private static decimal
            GetOptimizationPenalty(
                string action,
                PropertyMatchSalesAutomationOptimizationDto optimization)
        {
            var relevant =
                optimization.Recommendations
                    .Where(x =>
                        string.Equals(
                            x.Target,
                            action,
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();

            if (relevant.Count == 0)
                return 0;

            decimal penalty = 0;

            foreach (var item in relevant)
            {
                penalty += item.Priority switch
                {
                    "Critical" => 20m,
                    "High" => 12m,
                    "Medium" => 6m,
                    "Low" => 3m,
                    _ => 0m
                };
            }

            return Math.Clamp(
                penalty,
                0,
                40);
        }

        private static decimal CalculateConfidence(
            PropertyMatchNextBestActionDto action,
            PropertyMatchSalesAutomationAnalyticsDto analytics,
            decimal historicalSuccess,
            decimal optimizationPenalty)
        {
            var score =
                (action.ActionScore * 0.30m) +
                (action.ConversionProbability * 0.25m) +
                (historicalSuccess * 0.20m) +
                (analytics.HealthScore * 0.15m) +
                (action.PriorityScore * 0.10m);

            score -=
                optimizationPenalty * 0.50m;

            return Math.Clamp(
                score,
                0,
                100);
        }

        private static decimal CalculateDecisionScore(
            PropertyMatchNextBestActionDto action,
            PropertyMatchSalesAutomationAnalyticsDto analytics,
            decimal confidence,
            decimal historicalSuccess,
            decimal optimizationPenalty)
        {
            var score =
                (confidence * 0.30m) +
                (action.ActionScore * 0.20m) +
                (action.UrgencyScore * 0.15m) +
                (action.ConversionProbability * 0.15m) +
                (historicalSuccess * 0.10m) +
                (analytics.HealthScore * 0.10m);

            if (action.RequiresImmediateAction)
                score += 5m;

            score -= optimizationPenalty;

            return Math.Clamp(
                score,
                0,
                100);
        }

        private static string GetDecision(
            PropertyMatchNextBestActionDto action,
            decimal decisionScore,
            decimal confidence,
            decimal optimizationPenalty)
        {
            if (string.Equals(
                action.NextBestAction,
                "Wait",
                StringComparison.OrdinalIgnoreCase))
            {
                return "Hold";
            }

            if (optimizationPenalty >= 30)
            {
                return "Hold";
            }

            if (decisionScore < 35 ||
                confidence < 30)
            {
                return "Reject";
            }

            if (action.NextBestAction ==
                    "SendOffer" ||
                action.NextBestAction ==
                    "ScheduleMeeting")
            {
                return "RequireApproval";
            }

            if (decisionScore >= 75 &&
                confidence >= 70)
            {
                return "AutoExecute";
            }

            if (decisionScore >= 50)
            {
                return "RequireApproval";
            }

            return "Hold";
        }

        private static string GetPriority(
            decimal decisionScore,
            string urgency)
        {
            if (urgency == "Critical" ||
                decisionScore >= 90)
            {
                return "Critical";
            }

            if (urgency == "High" ||
                decisionScore >= 75)
            {
                return "High";
            }

            if (decisionScore >= 55)
                return "Medium";

            return "Low";
        }

        private static string BuildReason(
            PropertyMatchNextBestActionDto action,
            PropertyMatchSalesAutomationAnalyticsDto analytics,
            string decision,
            decimal confidence,
            decimal historicalSuccess,
            decimal optimizationPenalty)
        {
            return
                $"Aksiyon: {action.NextBestAction}. " +
                $"Karar: {decision}. " +
                $"Confidence: %{confidence:N2}. " +
                $"Geçmiş aksiyon başarı oranı: %{historicalSuccess:N2}. " +
                $"Conversion olasılığı: %{action.ConversionProbability:N2}. " +
                $"Urgency: {action.Urgency}. " +
                $"Sistem sağlık skoru: {analytics.HealthScore:N2}. " +
                $"Optimizasyon cezası: {optimizationPenalty:N2}.";
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

    public class PropertyMatchSalesAutomationDecisionDto
    {
        public int MatchId { get; set; }

        public int LeadId { get; set; }

        public int PropertyId { get; set; }

        public string Stage { get; set; }
            = string.Empty;

        public string RecommendedAction { get; set; }
            = string.Empty;

        public string ActionTitle { get; set; }
            = string.Empty;

        public string RecommendedChannel { get; set; }
            = string.Empty;

        public string Decision { get; set; }
            = string.Empty;

        public decimal DecisionScore { get; set; }

        public decimal ConfidenceScore { get; set; }

        public decimal HistoricalSuccessRate { get; set; }

        public decimal OptimizationPenalty { get; set; }

        public string Priority { get; set; }
            = string.Empty;

        public string Urgency { get; set; }
            = string.Empty;

        public decimal UrgencyScore { get; set; }

        public decimal ConversionProbability { get; set; }

        public decimal RiskScore { get; set; }

        public int ExecuteWithinHours { get; set; }

        public bool RequiresHumanApproval { get; set; }

        public bool CanAutoExecute { get; set; }

        public string Reason { get; set; }
            = string.Empty;

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationDecisionSummaryDto
    {
        public int TotalDecisions { get; set; }

        public int AutoExecuteDecisions { get; set; }

        public int ApprovalRequiredDecisions { get; set; }

        public int HoldDecisions { get; set; }

        public int RejectDecisions { get; set; }

        public int CriticalDecisions { get; set; }

        public int HighConfidenceDecisions { get; set; }

        public decimal AverageDecisionScore { get; set; }

        public decimal AverageConfidenceScore { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
