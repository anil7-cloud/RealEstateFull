using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation-audit-integration")]
    public class PropertyCustomerMatchSalesAutomationAuditIntegrationController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesAutomationAuditIntegrationService
                _service;

        public PropertyCustomerMatchSalesAutomationAuditIntegrationController(
            PropertyCustomerMatchSalesAutomationAuditIntegrationService service)
        {
            _service = service;
        }

        [HttpGet("summary")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationAuditCombinedSummaryDto>>
            GetSummary()
        {
            var result =
                await _service.GetSummaryAsync();

            return Ok(result);
        }

        [HttpGet("storage-health")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationAuditStorageHealthDto>>
            GetStorageHealth()
        {
            var result =
                await _service.GetStorageHealthAsync();

            return Ok(result);
        }

        [HttpGet("memory/recent")]
        public async Task<IActionResult>
            GetMemoryRecent(
                [FromQuery] int limit = 100)
        {
            var result =
                await _service.GetMemoryRecentAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("persistent/recent")]
        public async Task<IActionResult>
            GetPersistentRecent(
                [FromQuery] int limit = 100)
        {
            try
            {
                var result =
                    await _service.GetPersistentRecentAsync(
                        NormalizeLimit(limit));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    new
                    {
                        message =
                            "Kalıcı audit depolamasına erişilemedi.",

                        error =
                            ex.Message,

                        timestamp =
                            DateTime.UtcNow
                    });
            }
        }

        [HttpPost("write")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationAuditIntegrationResultDto>>
            Write(
                [FromBody]
                PropertyMatchSalesAutomationAuditIntegrationRequest request)
        {
            if (string.IsNullOrWhiteSpace(
                request.EventType))
            {
                return BadRequest(new
                {
                    message =
                        "EventType boş olamaz."
                });
            }

            if (string.IsNullOrWhiteSpace(
                request.Action))
            {
                return BadRequest(new
                {
                    message =
                        "Action boş olamaz."
                });
            }

            if (string.IsNullOrWhiteSpace(
                request.Status))
            {
                return BadRequest(new
                {
                    message =
                        "Status boş olamaz."
                });
            }

            var result =
                await _service.WriteAsync(
                    eventType:
                        request.EventType,

                    action:
                        request.Action,

                    status:
                        request.Status,

                    message:
                        request.Message,

                    matchId:
                        request.MatchId,

                    leadId:
                        request.LeadId,

                    propertyId:
                        request.PropertyId,

                    executionId:
                        request.ExecutionId,

                    actor:
                        request.Actor,

                    score:
                        request.Score,

                    metadata:
                        request.Metadata,

                    isSuccessful:
                        request.IsSuccessful,

                    errorCode:
                        request.ErrorCode,

                    errorMessage:
                        request.ErrorMessage);

            if (!result.Successful)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    result);
            }

            if (!result.FullyPersisted)
            {
                return StatusCode(
                    StatusCodes.Status207MultiStatus,
                    result);
            }

            return Ok(result);
        }

        [HttpPost("system-event")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationAuditIntegrationResultDto>>
            WriteSystemEvent(
                [FromBody]
                PropertyMatchSalesAutomationSystemAuditRequest request)
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
                        request.Metadata,

                    isSuccessful:
                        request.IsSuccessful,

                    errorCode:
                        request.ErrorCode,

                    errorMessage:
                        request.ErrorMessage);

            return Ok(result);
        }

        [HttpPost("failure")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationAuditIntegrationResultDto>>
            WriteFailure(
                [FromBody]
                PropertyMatchSalesAutomationFailureAuditRequest request)
        {
            if (string.IsNullOrWhiteSpace(
                request.ErrorMessage))
            {
                return BadRequest(new
                {
                    message =
                        "ErrorMessage boş olamaz."
                });
            }

            var result =
                await _service.WriteAsync(
                    eventType:
                        "Failure",

                    action:
                        request.Action,

                    status:
                        "Failed",

                    message:
                        request.ErrorMessage,

                    matchId:
                        request.MatchId,

                    leadId:
                        request.LeadId,

                    propertyId:
                        request.PropertyId,

                    executionId:
                        request.ExecutionId,

                    actor:
                        request.Actor,

                    score:
                        request.Score,

                    metadata:
                        request.Metadata,

                    isSuccessful:
                        false,

                    errorCode:
                        request.ErrorCode,

                    errorMessage:
                        request.ErrorMessage);

            return Ok(result);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult>
            GetDashboard()
        {
            var summary =
                await _service.GetSummaryAsync();

            var health =
                await _service.GetStorageHealthAsync();

            var memory =
                await _service.GetMemoryRecentAsync(10);

            object persistent;

            try
            {
                persistent =
                    await _service
                        .GetPersistentRecentAsync(10);
            }
            catch (Exception ex)
            {
                persistent =
                    new
                    {
                        available = false,
                        error = ex.Message
                    };
            }

            return Ok(new
            {
                storage = new
                {
                    health.Status,

                    health.Healthy,

                    health.MemoryAvailable,

                    health.PersistentAvailable
                },

                statistics = new
                {
                    memoryEntries =
                        summary.MemoryEntries,

                    persistentEntries =
                        summary.PersistentEntries,

                    memoryFailures =
                        summary.MemoryFailures,

                    persistentFailures =
                        summary.PersistentFailures,

                    last24HoursMemory =
                        summary.Last24HoursMemory,

                    last24HoursPersistent =
                        summary.Last24HoursPersistent
                },

                recent = new
                {
                    memory,
                    persistent
                },

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("ready")]
        public async Task<IActionResult>
            Ready()
        {
            var health =
                await _service.GetStorageHealthAsync();

            if (!health.PersistentAvailable)
            {
                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    new
                    {
                        ready = false,

                        status =
                            health.Status,

                        memoryAvailable =
                            health.MemoryAvailable,

                        persistentAvailable =
                            health.PersistentAvailable,

                        error =
                            health.PersistentError,

                        timestamp =
                            DateTime.UtcNow
                    });
            }

            return Ok(new
            {
                ready = true,

                status =
                    health.Status,

                memoryAvailable =
                    health.MemoryAvailable,

                persistentAvailable =
                    health.PersistentAvailable,

                timestamp =
                    DateTime.UtcNow
            });
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
        PropertyMatchSalesAutomationAuditIntegrationRequest
    {
        public string EventType { get; set; }
            = string.Empty;

        public string Action { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = string.Empty;

        public string Message { get; set; }
            = string.Empty;

        public int? MatchId { get; set; }

        public int? LeadId { get; set; }

        public int? PropertyId { get; set; }

        public Guid? ExecutionId { get; set; }

        public string Actor { get; set; }
            = "System";

        public decimal? Score { get; set; }

        public string Metadata { get; set; }
            = string.Empty;

        public bool IsSuccessful { get; set; }
            = true;

        public string? ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }
    }

    public class
        PropertyMatchSalesAutomationSystemAuditRequest
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

        public bool IsSuccessful { get; set; }
            = true;

        public string? ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }
    }

    public class
        PropertyMatchSalesAutomationFailureAuditRequest
    {
        public string Action { get; set; }
            = "Execution";

        public string ErrorMessage { get; set; }
            = string.Empty;

        public string? ErrorCode { get; set; }

        public int? MatchId { get; set; }

        public int? LeadId { get; set; }

        public int? PropertyId { get; set; }

        public Guid? ExecutionId { get; set; }

        public string Actor { get; set; }
            = "System";

        public decimal? Score { get; set; }

        public string Metadata { get; set; }
            = string.Empty;
    }
}
