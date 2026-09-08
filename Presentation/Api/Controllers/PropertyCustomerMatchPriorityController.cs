using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-priority")]
    public class PropertyCustomerMatchPriorityController : ControllerBase
    {
        private readonly PropertyCustomerMatchPriorityService _service;

        public PropertyCustomerMatchPriorityController(
            PropertyCustomerMatchPriorityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyMatchPriorityDto>>>
            GetAll([FromQuery] int limit = 50)
        {
            var result = await _service.GetPrioritiesAsync(limit);

            return Ok(result);
        }

        [HttpGet("{matchId:int}")]
        public async Task<ActionResult<PropertyMatchPriorityDto>>
            GetByMatch(int matchId)
        {
            if (matchId <= 0)
                return BadRequest("MatchId geçerli olmalıdır.");

            var result = await _service
                .GetMatchPriorityAsync(matchId);

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
        public async Task<ActionResult<List<PropertyMatchPriorityDto>>>
            GetForLead(
                int leadId,
                [FromQuery] int limit = 20)
        {
            if (leadId <= 0)
                return BadRequest("LeadId geçerli olmalıdır.");

            var result = await _service
                .GetLeadPrioritiesAsync(
                    leadId,
                    limit);

            return Ok(result);
        }

        [HttpGet("property/{propertyId:int}")]
        public async Task<ActionResult<List<PropertyMatchPriorityDto>>>
            GetForProperty(
                int propertyId,
                [FromQuery] int limit = 20)
        {
            if (propertyId <= 0)
                return BadRequest("PropertyId geçerli olmalıdır.");

            var result = await _service
                .GetPropertyPrioritiesAsync(
                    propertyId,
                    limit);

            return Ok(result);
        }

        [HttpGet("critical")]
        public async Task<ActionResult<List<PropertyMatchPriorityDto>>>
            GetCritical([FromQuery] int limit = 20)
        {
            var priorities = await _service
                .GetPrioritiesAsync(100);

            var result = priorities
                .Where(x => x.PriorityLevel == "Critical")
                .OrderByDescending(x => x.PriorityScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("high")]
        public async Task<ActionResult<List<PropertyMatchPriorityDto>>>
            GetHigh([FromQuery] int limit = 30)
        {
            var priorities = await _service
                .GetPrioritiesAsync(100);

            var result = priorities
                .Where(x =>
                    x.PriorityLevel == "Critical" ||
                    x.PriorityLevel == "VeryHigh" ||
                    x.PriorityLevel == "High")
                .OrderByDescending(x => x.PriorityScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("top")]
        public async Task<ActionResult<List<PropertyMatchPriorityDto>>>
            GetTop([FromQuery] int limit = 10)
        {
            var result = await _service
                .GetPrioritiesAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var priorities = await _service
                .GetPrioritiesAsync(100);

            var averagePriority =
                priorities.Count == 0
                    ? 0
                    : Math.Round(
                        priorities.Average(
                            x => x.PriorityScore),
                        2);

            return Ok(new
            {
                total = priorities.Count,

                critical = priorities.Count(
                    x => x.PriorityLevel == "Critical"),

                veryHigh = priorities.Count(
                    x => x.PriorityLevel == "VeryHigh"),

                high = priorities.Count(
                    x => x.PriorityLevel == "High"),

                medium = priorities.Count(
                    x => x.PriorityLevel == "Medium"),

                low = priorities.Count(
                    x => x.PriorityLevel == "Low"),

                veryLow = priorities.Count(
                    x => x.PriorityLevel == "VeryLow"),

                averagePriorityScore = averagePriority,

                generatedAt = DateTime.UtcNow
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service = "PropertyCustomerMatchPriority",
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
