using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation-runtime")]
    public class PropertyCustomerMatchSalesAutomationRuntimeController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesAutomationRuntimeService
                _runtimeService;

        public PropertyCustomerMatchSalesAutomationRuntimeController(
            PropertyCustomerMatchSalesAutomationRuntimeService runtimeService)
        {
            _runtimeService = runtimeService;
        }

        [HttpPost("run")]
        public async Task<
            ActionResult<PropertyCustomerMatchSalesAutomationRuntimeDto>>
            Run(
                [FromBody]
                PropertyCustomerMatchSalesAutomationRuntimeRequestDto request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    message = "Request boş olamaz.",
                    timestamp = DateTime.UtcNow
                });
            }

            if (request.MatchId <= 0)
            {
                return BadRequest(new
                {
                    message = "MatchId 0'dan büyük olmalıdır.",
                    timestamp = DateTime.UtcNow
                });
            }

            try
            {
                var result =
                    await _runtimeService
                        .RunAsync(request);

                if (result.Status == "AwaitingApproval")
                {
                    return Accepted(result);
                }

                if (!result.Successful)
                {
                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        result);
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        message =
                            "Runtime çalıştırılırken beklenmeyen hata oluştu.",

                        error =
                            ex.Message,

                        timestamp =
                            DateTime.UtcNow
                    });
            }
        }

        [HttpGet("execution/{executionId:guid}")]
        public async Task<IActionResult>
            GetExecution(
                Guid executionId)
        {
            var execution =
                await _runtimeService
                    .GetExecutionAsync(
                        executionId);

            if (execution == null)
            {
                return NotFound(new
                {
                    message =
                        "Execution bulunamadı.",

                    executionId,

                    timestamp =
                        DateTime.UtcNow
                });
            }

            return Ok(execution);
        }

        [HttpPost("execution/{executionId:guid}/retry")]
        public async Task<IActionResult>
            Retry(
                Guid executionId,
                [FromQuery]
                string actor = "System")
        {
            try
            {
                var execution =
                    await _runtimeService
                        .RetryAsync(
                            executionId,
                            actor);

                if (execution == null)
                {
                    return NotFound(new
                    {
                        message =
                            "Execution bulunamadı.",

                        executionId,

                        timestamp =
                            DateTime.UtcNow
                    });
                }

                return Ok(execution);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        message =
                            "Execution retry işlemi başarısız oldu.",

                        executionId,

                        error =
                            ex.Message,

                        timestamp =
                            DateTime.UtcNow
                    });
            }
        }

        [HttpGet("health")]
        public async Task<
            ActionResult<PropertyCustomerMatchSalesAutomationRuntimeHealthDto>>
            Health()
        {
            try
            {
                var health =
                    await _runtimeService
                        .GetHealthAsync();

                if (!health.Healthy)
                {
                    return StatusCode(
                        StatusCodes.Status503ServiceUnavailable,
                        health);
                }

                return Ok(health);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    new
                    {
                        healthy = false,

                        status = "Unavailable",

                        error = ex.Message,

                        checkedAt =
                            DateTime.UtcNow
                    });
            }
        }

        [HttpGet("ready")]
        public async Task<IActionResult>
            Ready()
        {
            try
            {
                var health =
                    await _runtimeService
                        .GetHealthAsync();

                var ready =
                    health.ExecutionServiceAvailable &&
                    health.AuditServiceAvailable;

                if (!ready)
                {
                    return StatusCode(
                        StatusCodes.Status503ServiceUnavailable,
                        new
                        {
                            ready = false,

                            health.Status,

                            health.ExecutionServiceAvailable,

                            health.AuditServiceAvailable,

                            health.DatabaseAvailable,

                            timestamp =
                                DateTime.UtcNow
                        });
                }

                return Ok(new
                {
                    ready = true,

                    health.Status,

                    health.ExecutionServiceAvailable,

                    health.AuditServiceAvailable,

                    health.DatabaseAvailable,

                    timestamp =
                        DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    new
                    {
                        ready = false,

                        error = ex.Message,

                        timestamp =
                            DateTime.UtcNow
                    });
            }
        }
    }
}
