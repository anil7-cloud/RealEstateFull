using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation-batch-runtime")]
    public class PropertyCustomerMatchSalesAutomationBatchRuntimeController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesAutomationBatchRuntimeService
                _batchRuntimeService;

        public PropertyCustomerMatchSalesAutomationBatchRuntimeController(
            PropertyCustomerMatchSalesAutomationBatchRuntimeService batchRuntimeService)
        {
            _batchRuntimeService =
                batchRuntimeService;
        }

        [HttpPost("run")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationBatchRuntimeResultDto>>
            Run(
                [FromBody]
                PropertyMatchSalesAutomationBatchRuntimeRequestDto request,
                CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    message =
                        "Request boş olamaz.",

                    timestamp =
                        DateTime.UtcNow
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

            if (!request.MatchIds.Any(x => x > 0))
            {
                return BadRequest(new
                {
                    message =
                        "En az bir geçerli MatchId gereklidir.",

                    timestamp =
                        DateTime.UtcNow
                });
            }

            try
            {
                var result =
                    await _batchRuntimeService
                        .RunAsync(
                            request,
                            cancellationToken);

                return result.Status switch
                {
                    "Completed" =>
                        Ok(result),

                    "PartiallyCompleted" =>
                        StatusCode(
                            StatusCodes.Status207MultiStatus,
                            result),

                    "Cancelled" =>
                        StatusCode(
                            StatusCodes.Status409Conflict,
                            result),

                    "Failed" =>
                        StatusCode(
                            StatusCodes.Status500InternalServerError,
                            result),

                    _ =>
                        Ok(result)
                };
            }
            catch (OperationCanceledException)
            {
                return StatusCode(
                    StatusCodes.Status409Conflict,
                    new
                    {
                        status =
                            "Cancelled",

                        message =
                            "Batch runtime işlemi iptal edildi.",

                        timestamp =
                            DateTime.UtcNow
                    });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message =
                        ex.Message,

                    timestamp =
                        DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        status =
                            "Failed",

                        message =
                            "Batch runtime sırasında beklenmeyen hata oluştu.",

                        error =
                            ex.Message,

                        timestamp =
                            DateTime.UtcNow
                    });
            }
        }

        [HttpPost("run-single")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationBatchRuntimeResultDto>>
            RunSingle(
                [FromQuery]
                int matchId,

                [FromQuery]
                bool executeImmediately = true,

                [FromQuery]
                bool requireApproval = false,

                [FromQuery]
                string actor = "System",

                CancellationToken cancellationToken = default)
        {
            if (matchId <= 0)
            {
                return BadRequest(new
                {
                    message =
                        "MatchId 0'dan büyük olmalıdır."
                });
            }

            var request =
                new PropertyMatchSalesAutomationBatchRuntimeRequestDto
                {
                    MatchIds =
                        new List<int>
                        {
                            matchId
                        },

                    Actor =
                        actor,

                    ExecuteImmediately =
                        executeImmediately,

                    RequireApproval =
                        requireApproval,

                    PersistAudit =
                        true,

                    MaxConcurrency =
                        1,

                    MaxBatchSize =
                        1
                };

            var result =
                await _batchRuntimeService
                    .RunAsync(
                        request,
                        cancellationToken);

            return Ok(result);
        }

        [HttpPost("validate")]
        public IActionResult
            Validate(
                [FromBody]
                PropertyMatchSalesAutomationBatchRuntimeRequestDto request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    valid =
                        false,

                    message =
                        "Request boş."
                });
            }

            var validIds =
                request.MatchIds?
                    .Where(x => x > 0)
                    .Distinct()
                    .ToList()
                ?? new List<int>();

            var invalidIds =
                request.MatchIds?
                    .Where(x => x <= 0)
                    .ToList()
                ?? new List<int>();

            var duplicates =
                request.MatchIds?
                    .Where(x => x > 0)
                    .GroupBy(x => x)
                    .Where(x => x.Count() > 1)
                    .Select(x => x.Key)
                    .ToList()
                ?? new List<int>();

            return Ok(new
            {
                valid =
                    validIds.Count > 0,

                totalReceived =
                    request.MatchIds?.Count ?? 0,

                validCount =
                    validIds.Count,

                invalidCount =
                    invalidIds.Count,

                duplicateCount =
                    duplicates.Count,

                validIds,

                invalidIds,

                duplicates,

                requestedConcurrency =
                    request.MaxConcurrency,

                effectiveConcurrency =
                    Math.Clamp(
                        request.MaxConcurrency,
                        1,
                        20),

                timestamp =
                    DateTime.UtcNow
            });
        }
    }
}
