using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-conversion-trend")]
    public class PropertyCustomerMatchSalesConversionTrendController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesConversionTrendService _service;

        public PropertyCustomerMatchSalesConversionTrendController(
            PropertyCustomerMatchSalesConversionTrendService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PropertyMatchConversionTrendDto>>
            GetTrend()
        {
            var result = await _service.GetTrendAsync();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<PropertyMatchConversionTrendDto>>
            GetSummary()
        {
            var result = await _service.GetTrendAsync();

            return Ok(result);
        }

        [HttpGet("items")]
        public async Task<
            ActionResult<List<PropertyMatchConversionTrendItemDto>>>
            GetItems([FromQuery] int limit = 50)
        {
            var result =
                await _service.GetTrendItemsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("positive-momentum")]
        public async Task<
            ActionResult<List<PropertyMatchConversionTrendItemDto>>>
            GetPositiveMomentum(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetPositiveMomentumAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("weak-momentum")]
        public async Task<
            ActionResult<List<PropertyMatchConversionTrendItemDto>>>
            GetWeakMomentum(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetWeakMomentumAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("rising")]
        public async Task<
            ActionResult<List<PropertyMatchConversionTrendItemDto>>>
            GetRising(
                [FromQuery] int limit = 30)
        {
            var items =
                await _service.GetTrendItemsAsync(100);

            var result = items
                .Where(x =>
                    string.Equals(
                        x.Direction,
                        "Rising",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.MomentumScore)
                .ThenByDescending(
                    x => x.ConversionProbability)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("declining")]
        public async Task<
            ActionResult<List<PropertyMatchConversionTrendItemDto>>>
            GetDeclining(
                [FromQuery] int limit = 30)
        {
            var items =
                await _service.GetTrendItemsAsync(100);

            var result = items
                .Where(x =>
                    string.Equals(
                        x.Direction,
                        "Declining",
                        StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.MomentumScore)
                .ThenBy(x => x.ConversionProbability)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("stable")]
        public async Task<
            ActionResult<List<PropertyMatchConversionTrendItemDto>>>
            GetStable(
                [FromQuery] int limit = 30)
        {
            var items =
                await _service.GetTrendItemsAsync(100);

            var result = items
                .Where(x =>
                    string.Equals(
                        x.Direction,
                        "Stable",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.MomentumScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("pipeline")]
        public async Task<IActionResult> GetPipeline()
        {
            var trend = await _service.GetTrendAsync();

            return Ok(new
            {
                totalMatches =
                    trend.TotalMatches,

                highProbability =
                    trend.HighProbabilityCount,

                mediumProbability =
                    trend.MediumProbabilityCount,

                lowProbability =
                    trend.LowProbabilityCount,

                predictedConversions =
                    trend.PredictedConversions,

                expectedConversions =
                    trend.ExpectedConversions,

                pipelineStrength =
                    trend.PipelineStrength,

                pipelineStrengthScore =
                    trend.PipelineStrengthScore,

                generatedAt =
                    trend.GeneratedAt
            });
        }

        [HttpGet("momentum")]
        public async Task<IActionResult> GetMomentum()
        {
            var trend = await _service.GetTrendAsync();

            return Ok(new
            {
                momentum =
                    trend.Momentum,

                momentumScore =
                    trend.MomentumScore,

                trend =
                    trend.Trend,

                averageConversionProbability =
                    trend.AverageConversionProbability,

                averageConfidence =
                    trend.AverageConfidence,

                generatedAt =
                    trend.GeneratedAt
            });
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var trendTask =
                _service.GetTrendAsync();

            var itemsTask =
                _service.GetTrendItemsAsync(100);

            await Task.WhenAll(
                trendTask,
                itemsTask);

            var trend =
                await trendTask;

            var items =
                await itemsTask;

            var strongest = items
                .OrderByDescending(x => x.MomentumScore)
                .Take(10)
                .ToList();

            var weakest = items
                .OrderBy(x => x.MomentumScore)
                .Take(10)
                .ToList();

            var rising = items
                .Where(x =>
                    string.Equals(
                        x.Direction,
                        "Rising",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.MomentumScore)
                .Take(10)
                .ToList();

            return Ok(new
            {
                trend,

                strongest,

                weakest,

                rising,

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
                    "PropertyCustomerMatchSalesConversionTrend",

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

            return Math.Min(
                limit,
                100);
        }
    }
}
