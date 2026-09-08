namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationPipelineService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationDecisionService
                _decisionService;

        private readonly
            PropertyCustomerMatchSalesAutomationOrchestratorService
                _orchestratorService;

        private readonly
            PropertyCustomerMatchSalesAutomationExecutionService
                _executionService;

        private readonly
            PropertyCustomerMatchSalesAutomationAnalyticsService
                _analyticsService;

        private readonly
            PropertyCustomerMatchSalesAutomationOptimizationService
                _optimizationService;

        public PropertyCustomerMatchSalesAutomationPipelineService(
            PropertyCustomerMatchSalesAutomationDecisionService decisionService,
            PropertyCustomerMatchSalesAutomationOrchestratorService orchestratorService,
            PropertyCustomerMatchSalesAutomationExecutionService executionService,
            PropertyCustomerMatchSalesAutomationAnalyticsService analyticsService,
            PropertyCustomerMatchSalesAutomationOptimizationService optimizationService)
        {
            _decisionService =
                decisionService;

            _orchestratorService =
                orchestratorService;

            _executionService =
                executionService;

            _analyticsService =
                analyticsService;

            _optimizationService =
                optimizationService;
        }

        public async Task<PropertyMatchSalesPipelineResultDto>
            RunAsync(
                int limit = 50,
                bool execute = false)
        {
            limit =
                NormalizeLimit(limit);

            var startedAt =
                DateTime.UtcNow;

            var decisions =
                await _decisionService
                    .GetDecisionsAsync(limit);

            PropertyMatchSalesOrchestrationResultDto?
                orchestration = null;

            if (execute)
            {
                orchestration =
                    await _orchestratorService
                        .OrchestrateAsync(limit);
            }

            var executionSummaryTask =
                _executionService.GetSummaryAsync();

            var analyticsTask =
                _analyticsService.GetAnalyticsAsync();

            var optimizationTask =
                _optimizationService.GetOptimizationAsync();

            await Task.WhenAll(
                executionSummaryTask,
                analyticsTask,
                optimizationTask);

            var executionSummary =
                await executionSummaryTask;

            var analytics =
                await analyticsTask;

            var optimization =
                await optimizationTask;

            var pipelineScore =
                CalculatePipelineScore(
                    decisions,
                    analytics,
                    optimization);

            return new PropertyMatchSalesPipelineResultDto
            {
                Status =
                    GetPipelineStatus(
                        pipelineScore),

                ExecuteMode =
                    execute,

                TotalDecisions =
                    decisions.Count,

                AutoExecuteDecisions =
                    decisions.Count(x =>
                        x.Decision ==
                        "AutoExecute"),

                ApprovalRequiredDecisions =
                    decisions.Count(x =>
                        x.Decision ==
                        "RequireApproval"),

                HoldDecisions =
                    decisions.Count(x =>
                        x.Decision ==
                        "Hold"),

                RejectDecisions =
                    decisions.Count(x =>
                        x.Decision ==
                        "Reject"),

                CriticalDecisions =
                    decisions.Count(x =>
                        x.Priority ==
                        "Critical"),

                HighConfidenceDecisions =
                    decisions.Count(x =>
                        x.ConfidenceScore >= 80),

                PipelineScore =
                    pipelineScore,

                Decisions =
                    decisions,

                Orchestration =
                    orchestration,

                ExecutionSummary =
                    executionSummary,

                Analytics =
                    analytics,

                Optimization =
                    optimization,

                StartedAt =
                    startedAt,

                CompletedAt =
                    DateTime.UtcNow
            };
        }

        public async Task<PropertyMatchSalesPipelineItemDto?>
            RunMatchAsync(
                int matchId,
                bool execute = false)
        {
            if (matchId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(matchId));
            }

            var decision =
                await _decisionService
                    .GetForMatchAsync(matchId);

            if (decision == null)
                return null;

            PropertyMatchSalesOrchestrationItemDto?
                orchestration = null;

            if (execute)
            {
                orchestration =
                    await _orchestratorService
                        .OrchestrateMatchAsync(
                            matchId);
            }

            return new PropertyMatchSalesPipelineItemDto
            {
                MatchId =
                    decision.MatchId,

                LeadId =
                    decision.LeadId,

                PropertyId =
                    decision.PropertyId,

                Decision =
                    decision.Decision,

                DecisionScore =
                    decision.DecisionScore,

                ConfidenceScore =
                    decision.ConfidenceScore,

                RecommendedAction =
                    decision.RecommendedAction,

                RecommendedChannel =
                    decision.RecommendedChannel,

                Priority =
                    decision.Priority,

                ExecuteMode =
                    execute,

                Orchestration =
                    orchestration,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        public async Task<List<PropertyMatchSalesPipelineItemDto>>
            RunLeadAsync(
                int leadId,
                int limit = 20,
                bool execute = false)
        {
            if (leadId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(leadId));
            }

            limit =
                NormalizeLimit(limit);

            var decisions =
                await _decisionService
                    .GetForLeadAsync(
                        leadId,
                        limit);

            List<PropertyMatchSalesOrchestrationItemDto>
                orchestrations = new();

            if (execute)
            {
                orchestrations =
                    await _orchestratorService
                        .OrchestrateLeadAsync(
                            leadId,
                            limit);
            }

            return decisions
                .Select(decision =>
                {
                    var orchestration =
                        orchestrations
                            .FirstOrDefault(x =>
                                x.MatchId ==
                                decision.MatchId);

                    return new
                        PropertyMatchSalesPipelineItemDto
                    {
                        MatchId =
                            decision.MatchId,

                        LeadId =
                            decision.LeadId,

                        PropertyId =
                            decision.PropertyId,

                        Decision =
                            decision.Decision,

                        DecisionScore =
                            decision.DecisionScore,

                        ConfidenceScore =
                            decision.ConfidenceScore,

                        RecommendedAction =
                            decision.RecommendedAction,

                        RecommendedChannel =
                            decision.RecommendedChannel,

                        Priority =
                            decision.Priority,

                        ExecuteMode =
                            execute,

                        Orchestration =
                            orchestration,

                        GeneratedAt =
                            DateTime.UtcNow
                    };
                })
                .OrderByDescending(
                    x => x.DecisionScore)
                .ToList();
        }

        public async Task<PropertyMatchSalesPipelineDashboardDto>
            GetDashboardAsync()
        {
            var decisionSummaryTask =
                _decisionService.GetSummaryAsync();

            var orchestrationSummaryTask =
                _orchestratorService.GetSummaryAsync();

            var executionSummaryTask =
                _executionService.GetSummaryAsync();

            var analyticsTask =
                _analyticsService.GetAnalyticsAsync();

            var optimizationTask =
                _optimizationService.GetOptimizationAsync();

            await Task.WhenAll(
                decisionSummaryTask,
                orchestrationSummaryTask,
                executionSummaryTask,
                analyticsTask,
                optimizationTask);

            var decisionSummary =
                await decisionSummaryTask;

            var orchestrationSummary =
                await orchestrationSummaryTask;

            var executionSummary =
                await executionSummaryTask;

            var analytics =
                await analyticsTask;

            var optimization =
                await optimizationTask;

            var score =
                CalculateDashboardScore(
                    decisionSummary,
                    analytics,
                    optimization);

            return new PropertyMatchSalesPipelineDashboardDto
            {
                PipelineScore =
                    score,

                PipelineStatus =
                    GetPipelineStatus(score),

                DecisionSummary =
                    decisionSummary,

                OrchestrationSummary =
                    orchestrationSummary,

                ExecutionSummary =
                    executionSummary,

                Analytics =
                    analytics,

                Optimization =
                    optimization,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        public async Task<PropertyMatchSalesPipelineHealthDto>
            GetHealthAsync()
        {
            var dashboard =
                await GetDashboardAsync();

            var issues =
                new List<string>();

            if (dashboard.Analytics.FailureRate >= 25)
            {
                issues.Add(
                    "Execution başarısızlık oranı yüksek.");
            }

            if (dashboard.Analytics.OverdueCount > 0)
            {
                issues.Add(
                    $"{dashboard.Analytics.OverdueCount} gecikmiş execution bulunuyor.");
            }

            if (dashboard.Optimization.HighPriorityChanges > 0)
            {
                issues.Add(
                    $"{dashboard.Optimization.HighPriorityChanges} yüksek öncelikli optimizasyon gerekiyor.");
            }

            if (dashboard.DecisionSummary.AverageConfidenceScore < 50 &&
                dashboard.DecisionSummary.TotalDecisions > 0)
            {
                issues.Add(
                    "Ortalama karar confidence skoru düşük.");
            }

            return new PropertyMatchSalesPipelineHealthDto
            {
                HealthScore =
                    dashboard.PipelineScore,

                Status =
                    dashboard.PipelineStatus,

                Healthy =
                    dashboard.PipelineScore >= 70,

                IssueCount =
                    issues.Count,

                Issues =
                    issues,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static decimal CalculatePipelineScore(
            List<PropertyMatchSalesAutomationDecisionDto>
                decisions,
            PropertyMatchSalesAutomationAnalyticsDto
                analytics,
            PropertyMatchSalesAutomationOptimizationDto
                optimization)
        {
            if (decisions.Count == 0)
                return 0;

            var averageDecision =
                decisions.Average(
                    x => x.DecisionScore);

            var averageConfidence =
                decisions.Average(
                    x => x.ConfidenceScore);

            var score =
                (averageDecision * 0.30m) +
                (averageConfidence * 0.25m) +
                (analytics.HealthScore * 0.25m) +
                (optimization.OptimizationScore * 0.20m);

            return Math.Round(
                Math.Clamp(
                    score,
                    0,
                    100),
                2);
        }

        private static decimal CalculateDashboardScore(
            PropertyMatchSalesAutomationDecisionSummaryDto
                decisions,
            PropertyMatchSalesAutomationAnalyticsDto
                analytics,
            PropertyMatchSalesAutomationOptimizationDto
                optimization)
        {
            if (decisions.TotalDecisions == 0)
                return 0;

            var score =
                (decisions.AverageDecisionScore *
                    0.30m) +
                (decisions.AverageConfidenceScore *
                    0.25m) +
                (analytics.HealthScore *
                    0.25m) +
                (optimization.OptimizationScore *
                    0.20m);

            return Math.Round(
                Math.Clamp(
                    score,
                    0,
                    100),
                2);
        }

        private static string GetPipelineStatus(
            decimal score)
        {
            return score switch
            {
                >= 90 => "Excellent",
                >= 80 => "Strong",
                >= 70 => "Healthy",
                >= 55 => "Moderate",
                >= 40 => "NeedsAttention",
                _ => "Critical"
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

    public class PropertyMatchSalesPipelineResultDto
    {
        public string Status { get; set; }
            = string.Empty;

        public bool ExecuteMode { get; set; }

        public int TotalDecisions { get; set; }

        public int AutoExecuteDecisions { get; set; }

        public int ApprovalRequiredDecisions { get; set; }

        public int HoldDecisions { get; set; }

        public int RejectDecisions { get; set; }

        public int CriticalDecisions { get; set; }

        public int HighConfidenceDecisions { get; set; }

        public decimal PipelineScore { get; set; }

        public List<PropertyMatchSalesAutomationDecisionDto>
            Decisions { get; set; } = new();

        public PropertyMatchSalesOrchestrationResultDto?
            Orchestration { get; set; }

        public PropertyMatchSalesAutomationExecutionSummaryDto?
            ExecutionSummary { get; set; }

        public PropertyMatchSalesAutomationAnalyticsDto?
            Analytics { get; set; }

        public PropertyMatchSalesAutomationOptimizationDto?
            Optimization { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime CompletedAt { get; set; }
    }

    public class PropertyMatchSalesPipelineItemDto
    {
        public int MatchId { get; set; }

        public int LeadId { get; set; }

        public int PropertyId { get; set; }

        public string Decision { get; set; }
            = string.Empty;

        public decimal DecisionScore { get; set; }

        public decimal ConfidenceScore { get; set; }

        public string RecommendedAction { get; set; }
            = string.Empty;

        public string RecommendedChannel { get; set; }
            = string.Empty;

        public string Priority { get; set; }
            = string.Empty;

        public bool ExecuteMode { get; set; }

        public PropertyMatchSalesOrchestrationItemDto?
            Orchestration { get; set; }

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesPipelineDashboardDto
    {
        public decimal PipelineScore { get; set; }

        public string PipelineStatus { get; set; }
            = string.Empty;

        public PropertyMatchSalesAutomationDecisionSummaryDto
            DecisionSummary { get; set; } = new();

        public PropertyMatchSalesOrchestrationSummaryDto
            OrchestrationSummary { get; set; } = new();

        public PropertyMatchSalesAutomationExecutionSummaryDto
            ExecutionSummary { get; set; } = new();

        public PropertyMatchSalesAutomationAnalyticsDto
            Analytics { get; set; } = new();

        public PropertyMatchSalesAutomationOptimizationDto
            Optimization { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesPipelineHealthDto
    {
        public decimal HealthScore { get; set; }

        public string Status { get; set; }
            = string.Empty;

        public bool Healthy { get; set; }

        public int IssueCount { get; set; }

        public List<string> Issues { get; set; }
            = new();

        public DateTime GeneratedAt { get; set; }
    }
}
