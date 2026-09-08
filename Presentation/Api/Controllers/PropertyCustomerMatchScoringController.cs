using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-scoring")]
    public class PropertyCustomerMatchScoringController : ControllerBase
    {
        private readonly PropertyCustomerMatchScoringService _scoringService;

        public PropertyCustomerMatchScoringController(
            PropertyCustomerMatchScoringService scoringService)
        {
            _scoringService = scoringService;
        }

        [HttpPost("calculate")]
        public ActionResult<PropertyMatchScoreResult> Calculate(
            [FromBody] PropertyMatchScoreRequest request)
        {
            if (request == null)
                return BadRequest("Eşleşme verisi gereklidir.");

            var result = _scoringService.Calculate(request);

            return Ok(result);
        }

        [HttpPost("calculate-and-create")]
        public ActionResult<PropertyMatchScoreResult> CalculateAndCreate(
            [FromBody] PropertyMatchScoreRequest request)
        {
            if (request == null)
                return BadRequest("Eşleşme verisi gereklidir.");

            var result = _scoringService.Calculate(request);

            return Ok(result);
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service = "PropertyCustomerMatchScoring",
                status = "Running",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
