using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation-audit")]
    public class PropertyCustomerMatchSalesAutomationAuditController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesAutomationAuditService _service;

        public PropertyCustomerMatchSalesAutomationAuditController(
            PropertyCustomerMatchSalesAutomationAuditService service)
        {
            _service = service;
        }

        [HttpGet("recent")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationAuditEntryDto>>>
            GetRecent([FromQuery] int limit = 100)
        {
            var result =
                await _service.GetRecentAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("{auditId:guid}")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationAuditEntryDto>>
            GetById(Guid auditId)
        {
            var result =
                await _service.GetByIdAsync(auditId);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Audit kaydı bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("match/{matchId:int}")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationAuditEntryDto>>>
            GetForMatch(
                int matchId,
                [FromQuery] int limit = 100)
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
                    matchId,
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationAuditEntryDto>>>
            GetForLead(
                int leadId,
                [FromQuery] int limit = 100)
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

        [HttpGet("execution/{executionId:guid}")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationAuditEntryDto>>>
            GetForExecution(
                Guid executionId,
                [FromQuery] int limit = 100)
        {
            var result =
                await _service.GetForExecutionAsync(
                    executionId,
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("failures")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationAuditEntryDto>>>
            GetFailures(
                [FromQuery] int limit = 50)
        {
            var result =
                await _service.GetFailuresAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationAuditSummaryDto>>
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
            var summaryTask =
                _service.GetSummaryAsync();

            var recentTask =
                _service.GetRecentAsync(20);

            var failuresTask =
                _service.GetFailuresAsync(10);

            await Task.WhenAll(
                summaryTask,
                recentTask,
                failuresTask);

            var summary =
                await summaryTask;

            var recent =
                await recentTask;

            var failures =
                await failuresTask;

            return Ok(new
            {
                summary,

                activity = new
                {
                    total =
                        summary.TotalEntries,

                    last24Hours =
                        summary.Last24Hours,

                    lastHour =
                        summary.LastHour
                },

                events = new
                {
                    decisions =
                        summary.DecisionEvents,

                    executions =
                        summary.ExecutionEvents,

                    orchestrations =
                        summary.OrchestrationEvents,

                    approvals =
                        summary.ApprovalEvents,

                    failures =
                        summary.FailureEvents,

                    system =
                        summary.SystemEvents
                },

                entities = new
                {
                    matches =
                        summary.UniqueMatches,

                    leads =
                        summary.UniqueLeads,

                    executions =
                        summary.UniqueExecutions
                },

                recent,

                recentFailures =
                    failures,

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("event/{eventType}")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationAuditEntryDto>>>
            GetByEventType(
                string eventType,
                [FromQuery] int limit = 50)
        {
            if (string.IsNullOrWhiteSpace(eventType))
            {
                return BadRequest(new
                {
                    message =
                        "EventType boş olamaz."
                });
            }

            var entries =
                await _service.GetRecentAsync(500);

            var result =
                entries
                    .Where(x =>
                        string.Equals(
                            x.EventType,
                            eventType,
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => x.CreatedAt)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationAuditEntryDto>>>
            GetByStatus(
                string status,
                [FromQuery] int limit = 50)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest(new
                {
                    message =
                        "Status boş olamaz."
                });
            }

            var entries =
                await _service.GetRecentAsync(500);

            var result =
                entries
                    .Where(x =>
                        string.Equals(
                            x.Status,
                            status,
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => x.CreatedAt)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("actor/{actor}")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationAuditEntryDto>>>
            GetByActor(
                string actor,
                [FromQuery] int limit = 50)
        {
            if (string.IsNullOrWhiteSpace(actor))
            {
                return BadRequest(new
                {
                    message =
                        "Actor boş olamaz."
                });
            }

            var entries =
                await _service.GetRecentAsync(500);

            var result =
                entries
                    .Where(x =>
                        string.Equals(
                            x.Actor,
                            actor,
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => x.CreatedAt)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("health")]
        public async Task<IActionResult>
            GetHealth()
        {
            var summary =
                await _service.GetSummaryAsync();

            var failureRate =
                Percentage(
                    summary.FailureEvents,
                    summary.TotalEntries);

            var status =
                failureRate switch
                {
                    >= 30m => "Critical",
                    >= 15m => "Warning",
                    >= 5m => "Stable",
                    _ => "Healthy"
                };

            return Ok(new
            {
                service =
                    "PropertyCustomerMatchSalesAutomationAudit",

                status,

                totalEntries =
                    summary.TotalEntries,

                failureEvents =
                    summary.FailureEvents,

                failureRate,

                last24Hours =
                    summary.Last24Hours,

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpPost("system-event")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationAuditEntryDto>>
            WriteSystemEvent(
                [FromBody]
                PropertyMatchSalesAutomationAuditSystemEventRequest request)
        {
            if (string.IsNullOrWhiteSpace(
                request.Message))
            {
                return BadRequest(new
                {
                    message =
                        "Message boş olamaz."
                });
            }

            var result =
                await _service.WriteAsync(
                    eventType:
                        "System",

                    action:
                        request.Action,

                    status:
                        request.Status,

                    message:
                        request.Message,

                    actor:
                        request.Actor,

                    metadata:
                        request.Metadata);

            return Ok(result);
        }

        private static decimal Percentage(
            int value,
            int total)
        {
            if (total <= 0)
                return 0;

            return Math.Round(
                ((decimal)value / total) *
                100m,
                2);
        }

        private static int NormalizeLimit(
            int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(
                limit,
                500);
        }
    }

    public class
        PropertyMatchSalesAutomationAuditSystemEventRequest
    {
        public string Action { get; set; }
            = "SystemEvent";

        public string Status { get; set; }
            = "Info";

        public string Message { get; set; }
            = string.Empty;

        public string Actor { get; set; }
            = "System";

        public string Metadata { get; set; }
            = string.Empty;
    }
}
