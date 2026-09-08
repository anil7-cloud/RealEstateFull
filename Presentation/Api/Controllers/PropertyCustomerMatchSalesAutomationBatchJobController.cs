using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation-batch-jobs")]
    public class PropertyCustomerMatchSalesAutomationBatchJobController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesAutomationBatchJobService
                _jobService;

        public PropertyCustomerMatchSalesAutomationBatchJobController(
            PropertyCustomerMatchSalesAutomationBatchJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost("run")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationBatchJobDto>>
            Run(
                [FromBody]
                PropertyMatchSalesAutomationBatchRuntimeRequestDto request,
                CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    message = "Request boş olamaz.",
                    timestamp = DateTime.UtcNow
                });
            }

            if (request.MatchIds == null ||
                request.MatchIds.Count == 0)
            {
                return BadRequest(new
                {
                    message =
                        "En az bir MatchId gönderilmelidir.",

                    timestamp =
                        DateTime.UtcNow
                });
            }

            try
            {
                var job =
                    await _jobService
                        .CreateAndRunAsync(
                            request,
                            cancellationToken);

                return job.Status switch
                {
                    "Completed" =>
                        Ok(job),

                    "PartiallyCompleted" =>
                        StatusCode(
                            StatusCodes.Status207MultiStatus,
                            job),

                    "Failed" =>
                        StatusCode(
                            StatusCodes.Status500InternalServerError,
                            job),

                    _ =>
                        Ok(job)
                };
            }
            catch (OperationCanceledException)
            {
                return StatusCode(
                    StatusCodes.Status409Conflict,
                    new
                    {
                        status = "Cancelled",

                        message =
                            "Batch job iptal edildi.",

                        timestamp =
                            DateTime.UtcNow
                    });
            }
        }

        [HttpGet("{jobId:guid}")]
        public async Task<IActionResult>
            Get(
                Guid jobId)
        {
            var job =
                await _jobService
                    .GetAsync(jobId);

            if (job == null)
            {
                return NotFound(new
                {
                    message =
                        "Batch job bulunamadı.",

                    jobId,

                    timestamp =
                        DateTime.UtcNow
                });
            }

            return Ok(job);
        }

        [HttpGet]
        public async Task<
            ActionResult<List<
                PropertyMatchSalesAutomationBatchJobDto>>>
            GetAll(
                [FromQuery]
                int limit = 100)
        {
            var jobs =
                await _jobService
                    .GetAllAsync(limit);

            return Ok(jobs);
        }

        [HttpGet("running")]
        public async Task<
            ActionResult<List<
                PropertyMatchSalesAutomationBatchJobDto>>>
            GetRunning()
        {
            var jobs =
                await _jobService
                    .GetRunningAsync();

            return Ok(jobs);
        }

        [HttpGet("failed")]
        public async Task<
            ActionResult<List<
                PropertyMatchSalesAutomationBatchJobDto>>>
            GetFailed(
                [FromQuery]
                int limit = 100)
        {
            var jobs =
                await _jobService
                    .GetFailedAsync(limit);

            return Ok(jobs);
        }

        [HttpGet("summary")]
        public async Task<
            ActionResult<
                PropertyMatchSalesAutomationBatchJobSummaryDto>>
            GetSummary()
        {
            var summary =
                await _jobService
                    .GetSummaryAsync();

            return Ok(summary);
        }

        [HttpDelete("{jobId:guid}")]
        public async Task<IActionResult>
            Remove(
                Guid jobId)
        {
            var removed =
                await _jobService
                    .RemoveAsync(jobId);

            if (!removed)
            {
                return NotFound(new
                {
                    removed = false,

                    message =
                        "Batch job bulunamadı.",

                    jobId
                });
            }

            return Ok(new
            {
                removed = true,

                jobId,

                timestamp =
                    DateTime.UtcNow
            });
        }

        [HttpDelete("completed")]
        public async Task<IActionResult>
            ClearCompleted()
        {
            var removed =
                await _jobService
                    .ClearCompletedAsync();

            return Ok(new
            {
                removed,

                timestamp =
                    DateTime.UtcNow
            });
        }

        [HttpGet("health")]
        public async Task<IActionResult>
            Health()
        {
            var summary =
                await _jobService
                    .GetSummaryAsync();

            return Ok(new
            {
                healthy = true,

                summary.TotalJobs,

                summary.RunningJobs,

                summary.CompletedJobs,

                summary.FailedJobs,

                summary.TotalProcessed,

                summary.SuccessRate,

                checkedAt =
                    DateTime.UtcNow
            });
        }
    }
}
