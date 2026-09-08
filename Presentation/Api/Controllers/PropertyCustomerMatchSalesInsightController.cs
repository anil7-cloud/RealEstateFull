using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-insight")]
    public class PropertyCustomerMatchSalesInsightController : ControllerBase
    {
        private readonly PropertyCustomerMatchSalesInsightService _service;

        public PropertyCustomerMatchSalesInsightController(
            PropertyCustomerMatchSalesInsightService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyMatchSalesInsightDto>>>
            GetAll([FromQuery] int limit = 50)
        {
            var result = await _service.GetInsightsAsync(limit);

            return Ok(result);
        }

        [HttpGet("match/{matchId:int}")]
        public async Task<ActionResult<PropertyMatchSalesInsightDto>>
            GetForMatch(int matchId)
        {
            if (matchId <= 0)
            {
                return BadRequest(new
                {
                    message = "MatchId geçerli olmalıdır."
                });
            }

            var result = await _service
                .GetMatchInsightAsync(matchId);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Eşleşme bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}")]
        public async Task<ActionResult<List<PropertyMatchSalesInsightDto>>>
            GetForLead(
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

            var result = await _service.GetLeadInsightsAsync(
                leadId,
                limit);

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<
            ActionResult<PropertyMatchSalesInsightSummaryDto>>
            GetSummary()
        {
            var result = await _service.GetSummaryAsync();

            return Ok(result);
        }

        [HttpGet("hot")]
        public async Task<ActionResult<List<PropertyMatchSalesInsightDto>>>
            GetHot([FromQuery] int limit = 20)
        {
            var insights = await _service.GetInsightsAsync(100);

            var result = insights
                .Where(x => x.InsightLevel == "Hot")
                .OrderByDescending(x => x.InsightScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("strong")]
        public async Task<ActionResult<List<PropertyMatchSalesInsightDto>>>
            GetStrong([FromQuery] int limit = 30)
        {
            var insights = await _service.GetInsightsAsync(100);

            var result = insights
                .Where(x =>
                    x.InsightLevel == "Hot" ||
                    x.InsightLevel == "Strong")
                .OrderByDescending(x => x.InsightScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("risk")]
        public async Task<ActionResult<List<PropertyMatchSalesInsightDto>>>
            GetRisky([FromQuery] int limit = 30)
        {
            var insights = await _service.GetInsightsAsync(100);

            var result = insights
                .Where(x =>
                    !string.Equals(
                        x.Risk,
                        "Belirgin risk bulunamadı.",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.InsightScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("full-match")]
        public async Task<ActionResult<List<PropertyMatchSalesInsightDto>>>
            GetFullMatches([FromQuery] int limit = 30)
        {
            var insights = await _service.GetInsightsAsync(100);

            var result = insights
                .Where(x => x.MatchedCriteriaCount >= 5)
                .OrderByDescending(x => x.InsightScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("top")]
        public async Task<ActionResult<List<PropertyMatchSalesInsightDto>>>
            GetTop([FromQuery] int limit = 10)
        {
            var result = await _service.GetInsightsAsync(
                NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service = "PropertyCustomerMatchSalesInsight",
                status = "Running",
                timestamp = DateTime.UtcNow
            });
        }

        private static int NormalizeLimit(int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(limit, 100);
        }
    }
}
