using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-revenue-forecast")]
    public class PropertyCustomerMatchRevenueForecastController : ControllerBase
    {
        private readonly PropertyCustomerMatchRevenueForecastService _service;

        public PropertyCustomerMatchRevenueForecastController(
            PropertyCustomerMatchRevenueForecastService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyMatchRevenueForecastDto>>>
            GetForecasts(
                [FromQuery] decimal propertyValue = 1_000_000m,
                [FromQuery] decimal commissionRate = 2m,
                [FromQuery] int limit = 50)
        {
            if (propertyValue < 0)
                return BadRequest("PropertyValue negatif olamaz.");

            if (commissionRate < 0 || commissionRate > 100)
                return BadRequest(
                    "CommissionRate 0 ile 100 arasında olmalıdır.");

            var result = await _service.GetForecastsAsync(
                propertyValue,
                commissionRate,
                limit);

            return Ok(result);
        }

        [HttpGet("match/{matchId:int}")]
        public async Task<ActionResult<PropertyMatchRevenueForecastDto>>
            GetMatchForecast(
                int matchId,
                [FromQuery] decimal propertyValue,
                [FromQuery] decimal commissionRate = 2m)
        {
            if (matchId <= 0)
                return BadRequest("MatchId geçerli olmalıdır.");

            if (propertyValue < 0)
                return BadRequest("PropertyValue negatif olamaz.");

            if (commissionRate < 0 || commissionRate > 100)
                return BadRequest(
                    "CommissionRate 0 ile 100 arasında olmalıdır.");

            var result = await _service.GetMatchForecastAsync(
                matchId,
                propertyValue,
                commissionRate);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Eşleşme bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<PropertyMatchRevenueSummaryDto>>
            GetSummary(
                [FromQuery] decimal propertyValue = 1_000_000m,
                [FromQuery] decimal commissionRate = 2m)
        {
            if (propertyValue < 0)
                return BadRequest("PropertyValue negatif olamaz.");

            if (commissionRate < 0 || commissionRate > 100)
                return BadRequest(
                    "CommissionRate 0 ile 100 arasında olmalıdır.");

            var result = await _service.GetSummaryAsync(
                propertyValue,
                commissionRate);

            return Ok(result);
        }

        [HttpGet("high-value")]
        public async Task<ActionResult<List<PropertyMatchRevenueForecastDto>>>
            GetHighValue(
                [FromQuery] decimal propertyValue = 1_000_000m,
                [FromQuery] decimal commissionRate = 2m,
                [FromQuery] decimal minimumRevenue = 10_000m,
                [FromQuery] int limit = 20)
        {
            if (propertyValue < 0)
                return BadRequest("PropertyValue negatif olamaz.");

            if (minimumRevenue < 0)
                return BadRequest("MinimumRevenue negatif olamaz.");

            var forecasts = await _service.GetForecastsAsync(
                propertyValue,
                commissionRate,
                100);

            limit = NormalizeLimit(limit);

            var result = forecasts
                .Where(x =>
                    x.ExpectedCommissionRevenue >= minimumRevenue)
                .OrderByDescending(x =>
                    x.ExpectedCommissionRevenue)
                .Take(limit)
                .ToList();

            return Ok(result);
        }

        [HttpGet("top")]
        public async Task<ActionResult<List<PropertyMatchRevenueForecastDto>>>
            GetTop(
                [FromQuery] decimal propertyValue = 1_000_000m,
                [FromQuery] decimal commissionRate = 2m,
                [FromQuery] int limit = 10)
        {
            var result = await _service.GetForecastsAsync(
                propertyValue,
                commissionRate,
                NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service = "PropertyCustomerMatchRevenueForecast",
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
