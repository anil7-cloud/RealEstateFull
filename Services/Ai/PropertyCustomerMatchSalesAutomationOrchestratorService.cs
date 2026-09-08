namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOrchestratorService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationDecisionService
                _decisionService;

        private readonly
            PropertyCustomerMatchSalesAutomationExecutionService
                _executionService;

        public PropertyCustomerMatchSalesAutomationOrchestratorService(
            PropertyCustomerMatchSalesAutomationDecisionService
                decisionService,
            PropertyCustomerMatchSalesAutomationExecutionService
                executionService)
        {
            _decisionService =
                decisionService;

            _executionService =
                executionService;
        }

        public async Task<
            PropertyMatchSalesOrchestrationResultDto>
            OrchestrateAsync(int limit = 50)
        {
            var decisions =
                await _decisionService.GetDecisionsAsync(
                    NormalizeLimit(limit));

            var result =
                new PropertyMatchSalesOrchestrationResultDto
                {
                    TotalDecisions =
                        decisions.Count,

                    StartedAt =
                        DateTime.UtcNow
                };

            foreach (var decision in decisions)
            {
                var item =
                    await ProcessDecisionAsync(decision);

                result.Items.Add(item);
            }

            result.AutoExecuteCount =
                result.Items.Count(x =>
                    x.Decision == "AutoExecute");

            result.ApprovalRequiredCount =
                result.Items.Count(x =>
                    x.Decision == "RequireApproval");

            result.HoldCount =
                result.Items.Count(x =>
                    x.Decision == "Hold");

            result.RejectCount =
                result.Items.Count(x =>
                    x.Decision == "Reject");

            result.ExecutionCreatedCount =
                result.Items.Count(x =>
                    x.ExecutionCreated);

            result.ExecutionStartedCount =
                result.Items.Count(x =>
                    x.ExecutionStarted);

            result.FailedCount =
                result.Items.Count(x =>
                    !string.IsNullOrWhiteSpace(
                        x.Error));

            result.CompletedAt =
                DateTime.UtcNow;

            return result;
        }

        public async Task<
            PropertyMatchSalesOrchestrationItemDto?>
            OrchestrateMatchAsync(int matchId)
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

            return await ProcessDecisionAsync(
                decision);
        }

        public async Task<
            List<PropertyMatchSalesOrchestrationItemDto>>
            OrchestrateLeadAsync(
                int leadId,
                int limit = 20)
        {
            if (leadId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(leadId));
            }

            var decisions =
                await _decisionService
                    .GetForLeadAsync(
                        leadId,
                        NormalizeLimit(limit));

            var result =
                new List<
                    PropertyMatchSalesOrchestrationItemDto>();

            foreach (var decision in decisions)
            {
                result.Add(
                    await ProcessDecisionAsync(
                        decision));
            }

            return result;
        }

        public async Task<
            PropertyMatchSalesOrchestrationItemDto?>
            ExecuteBestDecisionAsync()
        {
            var decision =
                await _decisionService
                    .GetBestDecisionAsync();

            if (decision == null)
                return null;

            return await ProcessDecisionAsync(
                decision);
        }

        public async Task<
            List<PropertyMatchSalesOrchestrationItemDto>>
            ExecuteAutoApprovedAsync(
                int limit = 20)
        {
            var decisions =
                await _decisionService
                    .GetAutoExecuteDecisionsAsync(
                        NormalizeLimit(limit));

            var result =
                new List<
                    PropertyMatchSalesOrchestrationItemDto>();

            foreach (var decision in decisions)
            {
                result.Add(
                    await ProcessDecisionAsync(
                        decision));
            }

            return result;
        }

        public async Task<
            PropertyMatchSalesOrchestrationSummaryDto>
            GetSummaryAsync()
        {
            var decisionSummaryTask =
                _decisionService.GetSummaryAsync();

            var executionSummaryTask =
                _executionService.GetSummaryAsync();

            await Task.WhenAll(
                decisionSummaryTask,
                executionSummaryTask);

            var decisions =
                await decisionSummaryTask;

            var executions =
                await executionSummaryTask;

            return new
                PropertyMatchSalesOrchestrationSummaryDto
            {
                TotalDecisions =
                    decisions.TotalDecisions,

                AutoExecuteDecisions =
                    decisions.AutoExecuteDecisions,

                ApprovalRequiredDecisions =
                    decisions.ApprovalRequiredDecisions,

                HoldDecisions =
                    decisions.HoldDecisions,

                RejectDecisions =
                    decisions.RejectDecisions,

                AverageDecisionScore =
                    decisions.AverageDecisionScore,

                AverageConfidenceScore =
                    decisions.AverageConfidenceScore,

                TotalExecutions =
                    executions.TotalExecutions,

                PendingExecutions =
                    executions.PendingExecutions,

                ApprovedExecutions =
                    executions.ApprovedExecutions,

                ExecutingExecutions =
                    executions.ExecutingExecutions,

                CompletedExecutions =
                    executions.CompletedExecutions,

                FailedExecutions =
                    executions.FailedExecutions,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private async Task<
            PropertyMatchSalesOrchestrationItemDto>
            ProcessDecisionAsync(
                PropertyMatchSalesAutomationDecisionDto decision)
        {
            var result =
                new PropertyMatchSalesOrchestrationItemDto
                {
                    MatchId =
                        decision.MatchId,

                    LeadId =
                        decision.LeadId,

                    PropertyId =
                        decision.PropertyId,

                    Decision =
                        decision.Decision,

                    RecommendedAction =
                        decision.RecommendedAction,

                    RecommendedChannel =
                        decision.RecommendedChannel,

                    DecisionScore =
                        decision.DecisionScore,

                    ConfidenceScore =
                        decision.ConfidenceScore,

                    Priority =
                        decision.Priority,

                    ProcessedAt =
                        DateTime.UtcNow
                };

            try
            {
                if (decision.Decision == "Reject")
                {
                    result.Status =
                        "Rejected";

                    result.Message =
                        "Karar motoru bu aksiyonu reddetti.";

                    return result;
                }

                if (decision.Decision == "Hold")
                {
                    result.Status =
                        "OnHold";

                    result.Message =
                        "Aksiyon beklemeye alındı.";

                    return result;
                }

                var execution =
                    await _executionService
                        .CreateForMatchAsync(
                            decision.MatchId);

                if (execution == null)
                {
                    result.Status =
                        "NoExecution";

                    result.Message =
                        "Execution oluşturulamadı.";

                    return result;
                }

                result.ExecutionCreated =
                    true;

                result.ExecutionId =
                    execution.ExecutionId;

                result.ExecutionStatus =
                    execution.Status;

                if (decision.Decision ==
                    "RequireApproval")
                {
                    result.Status =
                        "WaitingApproval";

                    result.Message =
                        "Execution oluşturuldu ve insan onayı bekliyor.";

                    return result;
                }

                if (decision.Decision ==
                    "AutoExecute")
                {
                    if (execution.RequiresApproval &&
                        execution.Status == "Pending")
                    {
                        execution =
                            await _executionService
                                .ApproveAsync(
                                    execution.ExecutionId,
                                    "AutomationOrchestrator");

                        if (execution == null)
                        {
                            result.Status =
                                "ApprovalFailed";

                            result.Error =
                                "Otomatik onay başarısız.";

                            return result;
                        }
                    }

                    var started =
                        await _executionService
                            .StartAsync(
                                execution.ExecutionId);

                    if (started == null)
                    {
                        result.Status =
                            "StartFailed";

                        result.Error =
                            "Execution başlatılamadı.";

                        return result;
                    }

                    result.ExecutionStatus =
                        started.Status;

                    if (started.Status ==
                        "Executing")
                    {
                        result.ExecutionStarted =
                            true;

                        result.Status =
                            "Executing";

                        result.Message =
                            "Satış otomasyonu başarıyla execution aşamasına geçirildi.";
                    }
                    else
                    {
                        result.Status =
                            "NotStarted";

                        result.Message =
                            string.IsNullOrWhiteSpace(
                                started.LastError)
                                ? "Execution başlatılamadı."
                                : started.LastError;
                    }

                    return result;
                }

                result.Status =
                    "Created";

                result.Message =
                    "Execution oluşturuldu.";

                return result;
            }
            catch (Exception ex)
            {
                result.Status =
                    "Failed";

                result.Error =
                    ex.Message;

                return result;
            }
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

    public class PropertyMatchSalesOrchestrationItemDto
    {
        public int MatchId { get; set; }

        public int LeadId { get; set; }

        public int PropertyId { get; set; }

        public string Decision { get; set; }
            = string.Empty;

        public string RecommendedAction { get; set; }
            = string.Empty;

        public string RecommendedChannel { get; set; }
            = string.Empty;

        public decimal DecisionScore { get; set; }

        public decimal ConfidenceScore { get; set; }

        public string Priority { get; set; }
            = string.Empty;

        public Guid? ExecutionId { get; set; }

        public bool ExecutionCreated { get; set; }

        public bool ExecutionStarted { get; set; }

        public string ExecutionStatus { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = string.Empty;

        public string Message { get; set; }
            = string.Empty;

        public string Error { get; set; }
            = string.Empty;

        public DateTime ProcessedAt { get; set; }
    }

    public class PropertyMatchSalesOrchestrationResultDto
    {
        public int TotalDecisions { get; set; }

        public int AutoExecuteCount { get; set; }

        public int ApprovalRequiredCount { get; set; }

        public int HoldCount { get; set; }

        public int RejectCount { get; set; }

        public int ExecutionCreatedCount { get; set; }

        public int ExecutionStartedCount { get; set; }

        public int FailedCount { get; set; }

        public List<PropertyMatchSalesOrchestrationItemDto>
            Items { get; set; } = new();

        public DateTime StartedAt { get; set; }

        public DateTime CompletedAt { get; set; }
    }

    public class PropertyMatchSalesOrchestrationSummaryDto
    {
        public int TotalDecisions { get; set; }

        public int AutoExecuteDecisions { get; set; }

        public int ApprovalRequiredDecisions { get; set; }

        public int HoldDecisions { get; set; }

        public int RejectDecisions { get; set; }

        public decimal AverageDecisionScore { get; set; }

        public decimal AverageConfidenceScore { get; set; }

        public int TotalExecutions { get; set; }

        public int PendingExecutions { get; set; }

        public int ApprovedExecutions { get; set; }

        public int ExecutingExecutions { get; set; }

        public int CompletedExecutions { get; set; }

        public int FailedExecutions { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
