using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-analytics")]
    public class PropertyCustomerMatchAnalyticsController : ControllerBase
    {
        private readonly PropertyCustomerMatchAnalyticsService _analyticsService;

        public PropertyCustomerMatchAnalyticsController(
            PropertyCustomerMatchAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet]
        public async Task<ActionResult<PropertyMatchAnalyticsDto>>
            GetAnalytics()
        {
            var result = await _analyticsService.GetAnalyticsAsync();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var result = await _analyticsService.GetAnalyticsAsync();

            return Ok(new
            {
                result.TotalMatches,
                result.AverageScore,
                result.StrongMatches,
                result.ExcellentMatches,
                result.WeakMatches,
                result.UniqueLeads,
                result.UniqueProperties,
                result.StrongMatchRate,
                result.ExcellentMatchRate,
                result.GeneratedAt
            });
        }

        [HttpGet("match-rates")]
        public async Task<IActionResult> GetMatchRates()
        {
            var result = await _analyticsService.GetAnalyticsAsync();

            return Ok(new
            {
                price = result.PriceMatchRate,
                location = result.LocationMatchRate,
                propertyType = result.PropertyTypeMatchRate,
                roomCount = result.RoomCountMatchRate,
                size = result.SizeMatchRate
            });
        }

        [HttpGet("score-distribution")]
        public async Task<IActionResult> GetScoreDistribution()
        {
            var result = await _analyticsService.GetAnalyticsAsync();

            return Ok(result.ScoreDistribution);
        }

        [HttpGet("top-leads")]
        public async Task<IActionResult> GetTopLeads(
            [FromQuery] int limit = 10)
        {
            var result = await _analyticsService.GetAnalyticsAsync();

            limit = NormalizeLimit(limit);

            return Ok(
                result.TopLeads
                    .Take(limit)
                    .ToList());
        }

        [HttpGet("top-properties")]
        public async Task<IActionResult> GetTopProperties(
            [FromQuery] int limit = 10)
        {
            var result = await _analyticsService.GetAnalyticsAsync();

            limit = NormalizeLimit(limit);

            return Ok(
                result.TopProperties
                    .Take(limit)
                    .ToList());
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service = "PropertyCustomerMatchAnalytics",
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
