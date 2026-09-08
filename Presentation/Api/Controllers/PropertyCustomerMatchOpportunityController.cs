using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-opportunity")]
    public class PropertyCustomerMatchOpportunityController : ControllerBase
    {
        private readonly PropertyCustomerMatchOpportunityService _service;

        public PropertyCustomerMatchOpportunityController(
            PropertyCustomerMatchOpportunityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyMatchOpportunityDto>>>
            GetAll(
                [FromQuery] decimal minimumScore = 60,
                [FromQuery] int limit = 50)
        {
            var result = await _service.GetOpportunitiesAsync(
                minimumScore,
                limit);

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}")]
        public async Task<ActionResult<List<PropertyMatchOpportunityDto>>>
            GetForLead(
                int leadId,
                [FromQuery] int limit = 20)
        {
            if (leadId <= 0)
                return BadRequest("LeadId geçerli olmalıdır.");

            var result = await _service.GetLeadOpportunitiesAsync(
                leadId,
                limit);

            return Ok(result);
        }

        [HttpGet("property/{propertyId:int}")]
        public async Task<ActionResult<List<PropertyMatchOpportunityDto>>>
            GetForProperty(
                int propertyId,
                [FromQuery] int limit = 20)
        {
            if (propertyId <= 0)
                return BadRequest("PropertyId geçerli olmalıdır.");

            var result = await _service.GetPropertyOpportunitiesAsync(
                propertyId,
                limit);

            return Ok(result);
        }

        [HttpGet("best")]
        public async Task<ActionResult<PropertyMatchOpportunityDto>>
            GetBest()
        {
            var result = await _service.GetBestOpportunityAsync();

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Aktif satış fırsatı bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("hot")]
        public async Task<ActionResult<List<PropertyMatchOpportunityDto>>>
            GetHot([FromQuery] int limit = 20)
        {
            var opportunities = await _service.GetOpportunitiesAsync(
                0,
                100);

            var result = opportunities
                .Where(x => x.OpportunityScore >= 80)
                .OrderByDescending(x => x.OpportunityScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("critical")]
        public async Task<ActionResult<List<PropertyMatchOpportunityDto>>>
            GetCritical([FromQuery] int limit = 20)
        {
            var opportunities = await _service.GetOpportunitiesAsync(
                0,
                100);

            var result = opportunities
                .Where(x =>
                    x.Priority == "Critical" ||
                    x.OpportunityScore >= 90)
                .OrderByDescending(x => x.OpportunityScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var opportunities = await _service.GetOpportunitiesAsync(
                0,
                100);

            var averageScore = opportunities.Count == 0
                ? 0
                : Math.Round(
                    opportunities.Average(x => x.OpportunityScore),
                    2);

            return Ok(new
            {
                total = opportunities.Count,

                critical = opportunities.Count(
                    x => x.Priority == "Critical"),

                veryHigh = opportunities.Count(
                    x => x.Priority == "VeryHigh"),

                high = opportunities.Count(
                    x => x.Priority == "High"),

                medium = opportunities.Count(
                    x => x.Priority == "Medium"),

                low = opportunities.Count(
                    x => x.Priority == "Low"),

                averageOpportunityScore = averageScore,

                generatedAt = DateTime.UtcNow
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service = "PropertyCustomerMatchOpportunity",
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
