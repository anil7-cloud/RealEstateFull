using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation-execution")]
    public class PropertyCustomerMatchSalesAutomationExecutionController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesAutomationExecutionService _service;

        public PropertyCustomerMatchSalesAutomationExecutionController(
            PropertyCustomerMatchSalesAutomationExecutionService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationExecutionDto>>>
            Create([FromQuery] int limit = 50)
        {
            var result =
                await _service.CreatePendingExecutionsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpPost("create/match/{matchId:int}")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationExecutionDto>>
            CreateForMatch(int matchId)
        {
            if (matchId <= 0)
            {
                return BadRequest(new
                {
                    message = "MatchId geçerli olmalıdır."
                });
            }

            var result =
                await _service.CreateForMatchAsync(matchId);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Bu eşleşme için otomasyon oluşturulamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationExecutionDto>>>
            GetAll()
        {
            var result =
                await _service.GetExecutionsAsync();

            return Ok(result);
        }

        [HttpGet("{executionId:guid}")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationExecutionDto>>
            GetById(Guid executionId)
        {
            var result =
                await _service.GetExecutionAsync(
                    executionId);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Execution bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("pending")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationExecutionDto>>>
            GetPending()
        {
            var result =
                await _service.GetPendingAsync();

            return Ok(result);
        }

        [HttpGet("approved")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationExecutionDto>>>
            GetApproved()
        {
            var result =
                await _service.GetApprovedAsync();

            return Ok(result);
        }

        [HttpGet("failed")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationExecutionDto>>>
            GetFailed()
        {
            var result =
                await _service.GetFailedAsync();

            return Ok(result);
        }

        [HttpPost("{executionId:guid}/approve")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationExecutionDto>>
            Approve(
                Guid executionId,
                [FromBody] ApproveExecutionRequest? request)
        {
            var approvedBy =
                string.IsNullOrWhiteSpace(
                    request?.ApprovedBy)
                    ? "System"
                    : request.ApprovedBy;

            var result =
                await _service.ApproveAsync(
                    executionId,
                    approvedBy);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Execution bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpPost("{executionId:guid}/start")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationExecutionDto>>
            Start(Guid executionId)
        {
            var result =
                await _service.StartAsync(
                    executionId);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Execution bulunamadı."
                });
            }

            if (result.RequiresApproval &&
                result.Status != "Executing")
            {
                return Conflict(new
                {
                    message =
                        result.LastError,

                    execution =
                        result
                });
            }

            return Ok(result);
        }

        [HttpPost("{executionId:guid}/complete")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationExecutionDto>>
            Complete(
                Guid executionId,
                [FromBody] CompleteExecutionRequest? request)
        {
            var result =
                await _service.CompleteAsync(
                    executionId,
                    request?.Result ?? string.Empty);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Execution bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpPost("{executionId:guid}/fail")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationExecutionDto>>
            Fail(
                Guid executionId,
                [FromBody] FailExecutionRequest? request)
        {
            var error =
                string.IsNullOrWhiteSpace(
                    request?.Error)
                    ? "Bilinmeyen otomasyon hatası."
                    : request.Error;

            var result =
                await _service.FailAsync(
                    executionId,
                    error);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Execution bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpPost("{executionId:guid}/retry")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationExecutionDto>>
            Retry(Guid executionId)
        {
            var result =
                await _service.RetryAsync(
                    executionId);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Execution bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpPost("{executionId:guid}/cancel")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationExecutionDto>>
            Cancel(
                Guid executionId,
                [FromBody] CancelExecutionRequest? request)
        {
            var result =
                await _service.CancelAsync(
                    executionId,
                    request?.Reason ?? string.Empty);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Execution bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("executing")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationExecutionDto>>>
            GetExecuting()
        {
            var executions =
                await _service.GetExecutionsAsync();

            var result =
                executions
                    .Where(x =>
                        string.Equals(
                            x.Status,
                            "Executing",
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => x.ExecutionScore)
                    .ToList();

            return Ok(result);
        }

        [HttpGet("completed")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationExecutionDto>>>
            GetCompleted(
                [FromQuery] int limit = 50)
        {
            var executions =
                await _service.GetExecutionsAsync();

            var result =
                executions
                    .Where(x =>
                        string.Equals(
                            x.Status,
                            "Completed",
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => x.CompletedAt)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("cancelled")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationExecutionDto>>>
            GetCancelled(
                [FromQuery] int limit = 50)
        {
            var executions =
                await _service.GetExecutionsAsync();

            var result =
                executions
                    .Where(x =>
                        string.Equals(
                            x.Status,
                            "Cancelled",
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => x.CancelledAt)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("approval-required")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationExecutionDto>>>
            GetApprovalRequired(
                [FromQuery] int limit = 50)
        {
            var executions =
                await _service.GetExecutionsAsync();

            var result =
                executions
                    .Where(x =>
                        x.RequiresApproval &&
                        x.Status == "Pending")
                    .OrderByDescending(
                        x => x.ExecutionScore)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("due")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationExecutionDto>>>
            GetDue(
                [FromQuery] int hours = 24,
                [FromQuery] int limit = 50)
        {
            hours =
                Math.Clamp(
                    hours,
                    1,
                    168);

            var maximumDate =
                DateTime.UtcNow.AddHours(hours);

            var executions =
                await _service.GetExecutionsAsync();

            var result =
                executions
                    .Where(x =>
                        x.ScheduledAt <= maximumDate &&
                        x.Status != "Completed" &&
                        x.Status != "Cancelled")
                    .OrderBy(
                        x => x.ScheduledAt)
                    .ThenByDescending(
                        x => x.ExecutionScore)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationExecutionSummaryDto>>
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
            var executionsTask =
                _service.GetExecutionsAsync();

            var pendingTask =
                _service.GetPendingAsync();

            var approvedTask =
                _service.GetApprovedAsync();

            var failedTask =
                _service.GetFailedAsync();

            var summaryTask =
                _service.GetSummaryAsync();

            await Task.WhenAll(
                executionsTask,
                pendingTask,
                approvedTask,
                failedTask,
                summaryTask);

            var executions =
                await executionsTask;

            var pending =
                await pendingTask;

            var approved =
                await approvedTask;

            var failed =
                await failedTask;

            var summary =
                await summaryTask;

            var executing =
                executions
                    .Where(x =>
                        x.Status == "Executing")
                    .OrderByDescending(
                        x => x.ExecutionScore)
                    .Take(10)
                    .ToList();

            var recentlyCompleted =
                executions
                    .Where(x =>
                        x.Status == "Completed")
                    .OrderByDescending(
                        x => x.CompletedAt)
                    .Take(10)
                    .ToList();

            var highestPriority =
                executions
                    .Where(x =>
                        x.Status != "Completed" &&
                        x.Status != "Cancelled")
                    .OrderByDescending(
                        x => x.ExecutionScore)
                    .Take(10)
                    .ToList();

            return Ok(new
            {
                summary,

                pending =
                    pending.Take(10),

                approved =
                    approved.Take(10),

                executing,

                failed =
                    failed.Take(10),

                recentlyCompleted,

                highestPriority,

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service =
                    "PropertyCustomerMatchSalesAutomationExecution",

                status =
                    "Running",

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
                100);
        }
    }

    public class ApproveExecutionRequest
    {
        public string ApprovedBy { get; set; }
            = "System";
    }

    public class CompleteExecutionRequest
    {
        public string Result { get; set; }
            = string.Empty;
    }

    public class FailExecutionRequest
    {
        public string Error { get; set; }
            = string.Empty;
    }

    public class CancelExecutionRequest
    {
        public string Reason { get; set; }
            = string.Empty;
    }
}
