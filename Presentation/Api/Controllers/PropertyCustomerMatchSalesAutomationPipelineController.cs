using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation-pipeline")]
    public class PropertyCustomerMatchSalesAutomationPipelineController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesAutomationPipelineService _service;

        public PropertyCustomerMatchSalesAutomationPipelineController(
            PropertyCustomerMatchSalesAutomationPipelineService service)
        {
            _service = service;
        }

        [HttpPost("run")]
        public async Task<
            ActionResult<PropertyMatchSalesPipelineResultDto>>
            Run(
                [FromQuery] int limit = 50,
                [FromQuery] bool execute = false)
        {
            var result =
                await _service.RunAsync(
                    NormalizeLimit(limit),
                    execute);

            return Ok(result);
        }

        [HttpPost("match/{matchId:int}")]
        public async Task<
            ActionResult<PropertyMatchSalesPipelineItemDto>>
            RunMatch(
                int matchId,
                [FromQuery] bool execute = false)
        {
            if (matchId <= 0)
            {
                return BadRequest(new
                {
                    message =
                        "MatchId geçerli olmalıdır."
                });
            }

            var result =
                await _service.RunMatchAsync(
                    matchId,
                    execute);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Bu eşleşme için pipeline sonucu bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpPost("lead/{leadId:int}")]
        public async Task<
            ActionResult<List<PropertyMatchSalesPipelineItemDto>>>
            RunLead(
                int leadId,
                [FromQuery] int limit = 20,
                [FromQuery] bool execute = false)
        {
            if (leadId <= 0)
            {
                return BadRequest(new
                {
                    message =
                        "LeadId geçerli olmalıdır."
                });
            }

            var result =
                await _service.RunLeadAsync(
                    leadId,
                    NormalizeLimit(limit),
                    execute);

            return Ok(result);
        }

        [HttpGet("dashboard")]
        public async Task<
            ActionResult<PropertyMatchSalesPipelineDashboardDto>>
            GetDashboard()
        {
            var result =
                await _service.GetDashboardAsync();

            return Ok(result);
        }

        [HttpGet("health")]
        public async Task<
            ActionResult<PropertyMatchSalesPipelineHealthDto>>
            GetHealth()
        {
            var result =
                await _service.GetHealthAsync();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult>
            GetSummary()
        {
            var dashboard =
                await _service.GetDashboardAsync();

            return Ok(new
            {
                pipeline = new
                {
                    score =
                        dashboard.PipelineScore,

                    status =
                        dashboard.PipelineStatus
                },

                decisions = new
                {
                    total =
                        dashboard.DecisionSummary
                            .TotalDecisions,

                    autoExecute =
                        dashboard.DecisionSummary
                            .AutoExecuteDecisions,

                    approvalRequired =
                        dashboard.DecisionSummary
                            .ApprovalRequiredDecisions,

                    hold =
                        dashboard.DecisionSummary
                            .HoldDecisions,

                    reject =
                        dashboard.DecisionSummary
                            .RejectDecisions,

                    averageScore =
                        dashboard.DecisionSummary
                            .AverageDecisionScore,

                    averageConfidence =
                        dashboard.DecisionSummary
                            .AverageConfidenceScore
                },

                executions = new
                {
                    total =
                        dashboard.ExecutionSummary
                            .TotalExecutions,

                    pending =
                        dashboard.ExecutionSummary
                            .PendingExecutions,

                    approved =
                        dashboard.ExecutionSummary
                            .ApprovedExecutions,

                    executing =
                        dashboard.ExecutionSummary
                            .ExecutingExecutions,

                    completed =
                        dashboard.ExecutionSummary
                            .CompletedExecutions,

                    failed =
                        dashboard.ExecutionSummary
                            .FailedExecutions
                },

                analytics = new
                {
                    successRate =
                        dashboard.Analytics
                            .SuccessRate,

                    failureRate =
                        dashboard.Analytics
                            .FailureRate,

                    overdue =
                        dashboard.Analytics
                            .OverdueCount,

                    healthScore =
                        dashboard.Analytics
                            .HealthScore,

                    healthLevel =
                        dashboard.Analytics
                            .HealthLevel
                },

                optimization = new
                {
                    score =
                        dashboard.Optimization
                            .OptimizationScore,

                    level =
                        dashboard.Optimization
                            .OptimizationLevel,

                    recommendedChanges =
                        dashboard.Optimization
                            .RecommendedChanges,

                    highPriorityChanges =
                        dashboard.Optimization
                            .HighPriorityChanges
                },

                generatedAt =
                    dashboard.GeneratedAt
            });
        }

        [HttpGet("kpi")]
        public async Task<IActionResult>
            GetKpi()
        {
            var dashboard =
                await _service.GetDashboardAsync();

            var decisions =
                dashboard.DecisionSummary;

            var executions =
                dashboard.ExecutionSummary;

            var analytics =
                dashboard.Analytics;

            return Ok(new
            {
                pipelineScore =
                    dashboard.PipelineScore,

                decisionQuality = new
                {
                    averageDecisionScore =
                        decisions.AverageDecisionScore,

                    averageConfidenceScore =
                        decisions.AverageConfidenceScore,

                    autoExecutionRate =
                        Percentage(
                            decisions.AutoExecuteDecisions,
                            decisions.TotalDecisions)
                },

                executionQuality = new
                {
                    completionRate =
                        Percentage(
                            executions.CompletedExecutions,
                            executions.TotalExecutions),

                    failureRate =
                        Percentage(
                            executions.FailedExecutions,
                            executions.TotalExecutions),

                    activeExecutions =
                        executions.PendingExecutions +
                        executions.ApprovedExecutions +
                        executions.ExecutingExecutions
                },

                businessPerformance = new
                {
                    successRate =
                        analytics.SuccessRate,

                    failureRate =
                        analytics.FailureRate,

                    overdueCount =
                        analytics.OverdueCount
                },

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("status")]
        public async Task<IActionResult>
            GetStatus()
        {
            var health =
                await _service.GetHealthAsync();

            return Ok(new
            {
                service =
                    "PropertyCustomerMatchSalesAutomationPipeline",

                status =
                    health.Status,

                healthy =
                    health.Healthy,

                score =
                    health.HealthScore,

                issues =
                    health.IssueCount,

                timestamp =
                    DateTime.UtcNow
            });
        }

        [HttpGet("issues")]
        public async Task<IActionResult>
            GetIssues()
        {
            var health =
                await _service.GetHealthAsync();

            return Ok(new
            {
                count =
                    health.IssueCount,

                issues =
                    health.Issues,

                healthScore =
                    health.HealthScore,

                status =
                    health.Status,

                generatedAt =
                    health.GeneratedAt
            });
        }

        [HttpPost("dry-run")]
        public async Task<
            ActionResult<PropertyMatchSalesPipelineResultDto>>
            DryRun(
                [FromQuery] int limit = 50)
        {
            var result =
                await _service.RunAsync(
                    NormalizeLimit(limit),
                    false);

            return Ok(result);
        }

        [HttpPost("execute")]
        public async Task<
            ActionResult<PropertyMatchSalesPipelineResultDto>>
            Execute(
                [FromQuery] int limit = 50)
        {
            var result =
                await _service.RunAsync(
                    NormalizeLimit(limit),
                    true);

            return Ok(result);
        }

        private static decimal Percentage(
            int value,
            int total)
        {
            if (total <= 0)
                return 0;

            return Math.Round(
                ((decimal)value / total) * 100m,
                2);
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
}
