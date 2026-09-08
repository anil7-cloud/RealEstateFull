using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation-orchestrator")]
    public class PropertyCustomerMatchSalesAutomationOrchestratorController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOrchestratorService _service;

        public PropertyCustomerMatchSalesAutomationOrchestratorController(
            PropertyCustomerMatchSalesAutomationOrchestratorService service)
        {
            _service = service;
        }

        [HttpPost("orchestrate")]
        public async Task<
            ActionResult<PropertyMatchSalesOrchestrationResultDto>>
            Orchestrate([FromQuery] int limit = 50)
        {
            var result =
                await _service.OrchestrateAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpPost("match/{matchId:int}")]
        public async Task<
            ActionResult<PropertyMatchSalesOrchestrationItemDto>>
            OrchestrateMatch(int matchId)
        {
            if (matchId <= 0)
            {
                return BadRequest(new
                {
                    message = "MatchId geçerli olmalıdır."
                });
            }

            var result =
                await _service.OrchestrateMatchAsync(
                    matchId);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Bu eşleşme için orchestration sonucu bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpPost("lead/{leadId:int}")]
        public async Task<
            ActionResult<List<PropertyMatchSalesOrchestrationItemDto>>>
            OrchestrateLead(
                int leadId,
                [FromQuery] int limit = 20)
        {
            if (leadId <= 0)
            {
                return BadRequest(new
                {
                    message = "LeadId geçerli olmalıdır."
                });
            }

            var result =
                await _service.OrchestrateLeadAsync(
                    leadId,
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpPost("best")]
        public async Task<
            ActionResult<PropertyMatchSalesOrchestrationItemDto>>
            ExecuteBest()
        {
            var result =
                await _service.ExecuteBestDecisionAsync();

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Çalıştırılabilecek satış kararı bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpPost("auto-execute")]
        public async Task<
            ActionResult<List<PropertyMatchSalesOrchestrationItemDto>>>
            ExecuteAutoApproved(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.ExecuteAutoApprovedAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<
            ActionResult<PropertyMatchSalesOrchestrationSummaryDto>>
            GetSummary()
        {
            var result =
                await _service.GetSummaryAsync();

            return Ok(result);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult>
            GetDashboard()
        {
            var summary =
                await _service.GetSummaryAsync();

            return Ok(new
            {
                decisions = new
                {
                    total =
                        summary.TotalDecisions,

                    autoExecute =
                        summary.AutoExecuteDecisions,

                    approvalRequired =
                        summary.ApprovalRequiredDecisions,

                    hold =
                        summary.HoldDecisions,

                    reject =
                        summary.RejectDecisions,

                    averageDecisionScore =
                        summary.AverageDecisionScore,

                    averageConfidenceScore =
                        summary.AverageConfidenceScore
                },

                executions = new
                {
                    total =
                        summary.TotalExecutions,

                    pending =
                        summary.PendingExecutions,

                    approved =
                        summary.ApprovedExecutions,

                    executing =
                        summary.ExecutingExecutions,

                    completed =
                        summary.CompletedExecutions,

                    failed =
                        summary.FailedExecutions
                },

                generatedAt =
                    summary.GeneratedAt
            });
        }

        [HttpGet("status")]
        public async Task<IActionResult>
            GetStatus()
        {
            var summary =
                await _service.GetSummaryAsync();

            var activeExecutions =
                summary.PendingExecutions +
                summary.ApprovedExecutions +
                summary.ExecutingExecutions;

            return Ok(new
            {
                status =
                    "Running",

                totalDecisions =
                    summary.TotalDecisions,

                totalExecutions =
                    summary.TotalExecutions,

                activeExecutions,

                completedExecutions =
                    summary.CompletedExecutions,

                failedExecutions =
                    summary.FailedExecutions,

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("kpi")]
        public async Task<IActionResult>
            GetKpi()
        {
            var summary =
                await _service.GetSummaryAsync();

            var executionCompletionRate =
                Percentage(
                    summary.CompletedExecutions,
                    summary.TotalExecutions);

            var executionFailureRate =
                Percentage(
                    summary.FailedExecutions,
                    summary.TotalExecutions);

            var autoExecutionRate =
                Percentage(
                    summary.AutoExecuteDecisions,
                    summary.TotalDecisions);

            var approvalRate =
                Percentage(
                    summary.ApprovalRequiredDecisions,
                    summary.TotalDecisions);

            return Ok(new
            {
                decision = new
                {
                    averageScore =
                        summary.AverageDecisionScore,

                    averageConfidence =
                        summary.AverageConfidenceScore,

                    autoExecutionRate,

                    approvalRate
                },

                execution = new
                {
                    completionRate =
                        executionCompletionRate,

                    failureRate =
                        executionFailureRate,

                    active =
                        summary.PendingExecutions +
                        summary.ApprovedExecutions +
                        summary.ExecutingExecutions
                },

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("health")]
        public async Task<IActionResult>
            GetHealth()
        {
            var summary =
                await _service.GetSummaryAsync();

            var failureRate =
                Percentage(
                    summary.FailedExecutions,
                    summary.TotalExecutions);

            var healthScore =
                CalculateHealthScore(
                    summary,
                    failureRate);

            return Ok(new
            {
                service =
                    "PropertyCustomerMatchSalesAutomationOrchestrator",

                status =
                    GetHealthStatus(healthScore),

                healthScore,

                failureRate,

                averageDecisionScore =
                    summary.AverageDecisionScore,

                averageConfidenceScore =
                    summary.AverageConfidenceScore,

                timestamp =
                    DateTime.UtcNow
            });
        }

        private static decimal CalculateHealthScore(
            PropertyMatchSalesOrchestrationSummaryDto summary,
            decimal failureRate)
        {
            if (summary.TotalDecisions == 0)
                return 0;

            var decisionComponent =
                summary.AverageDecisionScore *
                0.35m;

            var confidenceComponent =
                summary.AverageConfidenceScore *
                0.30m;

            var failureComponent =
                (100m - failureRate) *
                0.25m;

            var executionComponent =
                summary.TotalExecutions == 0
                    ? 5m
                    : Percentage(
                        summary.CompletedExecutions,
                        summary.TotalExecutions) *
                      0.10m;

            return Math.Round(
                Math.Clamp(
                    decisionComponent +
                    confidenceComponent +
                    failureComponent +
                    executionComponent,
                    0,
                    100),
                2);
        }

        private static string GetHealthStatus(
            decimal score)
        {
            return score switch
            {
                >= 85 => "Excellent",
                >= 70 => "Healthy",
                >= 55 => "Moderate",
                >= 40 => "NeedsAttention",
                _ => "Critical"
            };
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

            return Math.Min(limit, 100);
        }
    }
}
