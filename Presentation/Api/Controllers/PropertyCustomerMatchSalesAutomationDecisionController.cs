using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation-decision")]
    public class PropertyCustomerMatchSalesAutomationDecisionController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesAutomationDecisionService _service;

        public PropertyCustomerMatchSalesAutomationDecisionController(
            PropertyCustomerMatchSalesAutomationDecisionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDecisionDto>>>
            GetAll([FromQuery] int limit = 50)
        {
            var result =
                await _service.GetDecisionsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("best")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationDecisionDto>>
            GetBest()
        {
            var result =
                await _service.GetBestDecisionAsync();

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Satış otomasyonu kararı bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("match/{matchId:int}")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationDecisionDto>>
            GetMatch(int matchId)
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
                await _service.GetForMatchAsync(
                    matchId);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Bu eşleşme için karar bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDecisionDto>>>
            GetLead(
                int leadId,
                [FromQuery] int limit = 20)
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
                await _service.GetForLeadAsync(
                    leadId,
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("auto-execute")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDecisionDto>>>
            GetAutoExecute(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service
                    .GetAutoExecuteDecisionsAsync(
                        NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("approval-required")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDecisionDto>>>
            GetApprovalRequired(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service
                    .GetApprovalRequiredDecisionsAsync(
                        NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("hold")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDecisionDto>>>
            GetHold(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetHoldDecisionsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("reject")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDecisionDto>>>
            GetReject(
                [FromQuery] int limit = 20)
        {
            var decisions =
                await _service.GetDecisionsAsync(100);

            var result =
                decisions
                    .Where(x =>
                        string.Equals(
                            x.Decision,
                            "Reject",
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => x.DecisionScore)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("critical")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDecisionDto>>>
            GetCritical(
                [FromQuery] int limit = 20)
        {
            var decisions =
                await _service.GetDecisionsAsync(100);

            var result =
                decisions
                    .Where(x =>
                        string.Equals(
                            x.Priority,
                            "Critical",
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => x.DecisionScore)
                    .ThenByDescending(
                        x => x.ConfidenceScore)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("high-priority")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDecisionDto>>>
            GetHighPriority(
                [FromQuery] int limit = 30)
        {
            var decisions =
                await _service.GetDecisionsAsync(100);

            var result =
                decisions
                    .Where(x =>
                        x.Priority == "Critical" ||
                        x.Priority == "High")
                    .OrderByDescending(
                        x => PriorityOrder(x.Priority))
                    .ThenByDescending(
                        x => x.DecisionScore)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("high-confidence")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDecisionDto>>>
            GetHighConfidence(
                [FromQuery] decimal minimum = 80,
                [FromQuery] int limit = 30)
        {
            minimum =
                Math.Clamp(
                    minimum,
                    0m,
                    100m);

            var decisions =
                await _service.GetDecisionsAsync(100);

            var result =
                decisions
                    .Where(x =>
                        x.ConfidenceScore >= minimum)
                    .OrderByDescending(
                        x => x.ConfidenceScore)
                    .ThenByDescending(
                        x => x.DecisionScore)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("action/{action}")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDecisionDto>>>
            GetByAction(
                string action,
                [FromQuery] int limit = 20)
        {
            if (string.IsNullOrWhiteSpace(action))
            {
                return BadRequest(new
                {
                    message =
                        "Action boş olamaz."
                });
            }

            var decisions =
                await _service.GetDecisionsAsync(100);

            var result =
                decisions
                    .Where(x =>
                        string.Equals(
                            x.RecommendedAction,
                            action,
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => x.DecisionScore)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("channel/{channel}")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDecisionDto>>>
            GetByChannel(
                string channel,
                [FromQuery] int limit = 20)
        {
            if (string.IsNullOrWhiteSpace(channel))
            {
                return BadRequest(new
                {
                    message =
                        "Channel boş olamaz."
                });
            }

            var decisions =
                await _service.GetDecisionsAsync(100);

            var result =
                decisions
                    .Where(x =>
                        x.RecommendedChannel.Contains(
                            channel,
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => x.DecisionScore)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationDecisionSummaryDto>>
            GetSummary()
        {
            var result =
                await _service.GetSummaryAsync();

            return Ok(result);
        }

        [HttpGet("kpi")]
        public async Task<IActionResult>
            GetKpi()
        {
            var summary =
                await _service.GetSummaryAsync();

            return Ok(new
            {
                total =
                    summary.TotalDecisions,

                execution = new
                {
                    autoExecute =
                        summary.AutoExecuteDecisions,

                    approvalRequired =
                        summary.ApprovalRequiredDecisions,

                    hold =
                        summary.HoldDecisions,

                    reject =
                        summary.RejectDecisions
                },

                quality = new
                {
                    critical =
                        summary.CriticalDecisions,

                    highConfidence =
                        summary.HighConfidenceDecisions,

                    averageDecisionScore =
                        summary.AverageDecisionScore,

                    averageConfidenceScore =
                        summary.AverageConfidenceScore
                },

                generatedAt =
                    summary.GeneratedAt
            });
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult>
            GetDashboard()
        {
            var summaryTask =
                _service.GetSummaryAsync();

            var decisionsTask =
                _service.GetDecisionsAsync(100);

            var autoExecuteTask =
                _service.GetAutoExecuteDecisionsAsync(10);

            var approvalTask =
                _service
                    .GetApprovalRequiredDecisionsAsync(10);

            var holdTask =
                _service.GetHoldDecisionsAsync(10);

            await Task.WhenAll(
                summaryTask,
                decisionsTask,
                autoExecuteTask,
                approvalTask,
                holdTask);

            var summary =
                await summaryTask;

            var decisions =
                await decisionsTask;

            var autoExecute =
                await autoExecuteTask;

            var approvalRequired =
                await approvalTask;

            var hold =
                await holdTask;

            var topDecisions =
                decisions
                    .OrderByDescending(
                        x => x.DecisionScore)
                    .Take(10)
                    .ToList();

            var highConfidence =
                decisions
                    .Where(x =>
                        x.ConfidenceScore >= 80)
                    .OrderByDescending(
                        x => x.ConfidenceScore)
                    .Take(10)
                    .ToList();

            var critical =
                decisions
                    .Where(x =>
                        x.Priority == "Critical")
                    .OrderByDescending(
                        x => x.DecisionScore)
                    .Take(10)
                    .ToList();

            return Ok(new
            {
                summary,

                autoExecute,

                approvalRequired,

                hold,

                topDecisions,

                highConfidence,

                critical,

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("service-health")]
        public IActionResult ServiceHealth()
        {
            return Ok(new
            {
                service =
                    "PropertyCustomerMatchSalesAutomationDecision",

                status =
                    "Running",

                timestamp =
                    DateTime.UtcNow
            });
        }

        private static int PriorityOrder(
            string priority)
        {
            return priority switch
            {
                "Critical" => 4,
                "High" => 3,
                "Medium" => 2,
                "Low" => 1,
                _ => 0
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
}
