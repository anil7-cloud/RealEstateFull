using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-forecast")]
    public class PropertyCustomerMatchSalesForecastController
        : ControllerBase
    {
        private readonly PropertyCustomerMatchSalesForecastService _service;

        public PropertyCustomerMatchSalesForecastController(
            PropertyCustomerMatchSalesForecastService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PropertyMatchSalesForecastDto>>
            GetForecast()
        {
            var result = await _service.GetForecastAsync();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<PropertyMatchSalesForecastDto>>
            GetSummary()
        {
            var result = await _service.GetForecastAsync();

            return Ok(result);
        }

        [HttpGet("items")]
        public async Task<
            ActionResult<List<PropertyMatchSalesForecastItemDto>>>
            GetItems([FromQuery] int limit = 50)
        {
            var result =
                await _service.GetForecastItemsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("likely-sales")]
        public async Task<
            ActionResult<List<PropertyMatchSalesForecastItemDto>>>
            GetLikelySales([FromQuery] int limit = 20)
        {
            var result =
                await _service.GetLikelySalesAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("critical-opportunities")]
        public async Task<
            ActionResult<List<PropertyMatchSalesForecastItemDto>>>
            GetCriticalOpportunities(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetCriticalOpportunitiesAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("stages")]
        public async Task<
            ActionResult<PropertyMatchSalesForecastStageDto>>
            GetStageForecast()
        {
            var result =
                await _service.GetStageForecastAsync();

            return Ok(result);
        }

        [HttpGet("very-high")]
        public async Task<
            ActionResult<List<PropertyMatchSalesForecastItemDto>>>
            GetVeryHigh([FromQuery] int limit = 20)
        {
            var items =
                await _service.GetForecastItemsAsync(100);

            var result = items
                .Where(x =>
                    string.Equals(
                        x.ForecastLevel,
                        "VeryHigh",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.ForecastScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("high")]
        public async Task<
            ActionResult<List<PropertyMatchSalesForecastItemDto>>>
            GetHigh([FromQuery] int limit = 30)
        {
            var items =
                await _service.GetForecastItemsAsync(100);

            var result = items
                .Where(x =>
                    x.ForecastLevel == "VeryHigh" ||
                    x.ForecastLevel == "High")
                .OrderByDescending(x => x.ForecastScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("top")]
        public async Task<
            ActionResult<List<PropertyMatchSalesForecastItemDto>>>
            GetTop([FromQuery] int limit = 10)
        {
            var items =
                await _service.GetForecastItemsAsync(
                    NormalizeLimit(limit));

            return Ok(items);
        }

        [HttpGet("pipeline")]
        public async Task<IActionResult> GetPipeline()
        {
            var forecast =
                await _service.GetForecastAsync();

            return Ok(new
            {
                totalMatches =
                    forecast.TotalMatches,

                activeOpportunities =
                    forecast.ActiveOpportunities,

                offerStage =
                    forecast.OfferStage,

                meetingStage =
                    forecast.MeetingStage,

                highProbabilityOpportunities =
                    forecast.HighProbabilityOpportunities,

                veryHighProbabilityOpportunities =
                    forecast.VeryHighProbabilityOpportunities,

                pipelineHealth =
                    forecast.PipelineHealth,

                generatedAt =
                    forecast.GeneratedAt
            });
        }

        [HttpGet("expected-sales")]
        public async Task<IActionResult> GetExpectedSales()
        {
            var forecast =
                await _service.GetForecastAsync();

            return Ok(new
            {
                wonSales =
                    forecast.WonSales,

                expectedActiveConversions =
                    forecast.ExpectedActiveConversions,

                expectedTotalSales =
                    forecast.ExpectedTotalSales,

                averageConversionProbability =
                    forecast.AverageConversionProbability,

                forecastScore =
                    forecast.ForecastScore,

                forecastLevel =
                    forecast.ForecastLevel,

                generatedAt =
                    forecast.GeneratedAt
            });
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var forecastTask =
                _service.GetForecastAsync();

            var itemsTask =
                _service.GetForecastItemsAsync(100);

            var stagesTask =
                _service.GetStageForecastAsync();

            await Task.WhenAll(
                forecastTask,
                itemsTask,
                stagesTask);

            var forecast =
                await forecastTask;

            var items =
                await itemsTask;

            var stages =
                await stagesTask;

            var strongestOpportunities = items
                .OrderByDescending(x => x.ForecastScore)
                .Take(10)
                .ToList();

            var likelySales = items
                .Where(x =>
                    x.ConversionProbability >= 70)
                .OrderByDescending(
                    x => x.ConversionProbability)
                .Take(10)
                .ToList();

            return Ok(new
            {
                forecast,

                stages,

                strongestOpportunities,

                likelySales,

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
                    "PropertyCustomerMatchSalesForecast",

                status =
                    "Running",

                timestamp =
                    DateTime.UtcNow
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
