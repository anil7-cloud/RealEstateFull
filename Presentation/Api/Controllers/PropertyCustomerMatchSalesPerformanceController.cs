using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-performance")]
    public class PropertyCustomerMatchSalesPerformanceController
        : ControllerBase
    {
        private readonly PropertyCustomerMatchSalesPerformanceService _service;

        public PropertyCustomerMatchSalesPerformanceController(
            PropertyCustomerMatchSalesPerformanceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PropertyMatchSalesPerformanceDto>>
            GetPerformance()
        {
            var result = await _service.GetPerformanceAsync();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<PropertyMatchSalesPerformanceDto>>
            GetSummary()
        {
            var result = await _service.GetPerformanceAsync();

            return Ok(result);
        }

        [HttpGet("stages")]
        public async Task<
            ActionResult<List<PropertyMatchStagePerformanceDto>>>
            GetStagePerformance()
        {
            var result =
                await _service.GetStagePerformanceAsync();

            return Ok(result);
        }

        [HttpGet("top")]
        public async Task<
            ActionResult<List<PropertyMatchSalesPerformanceItemDto>>>
            GetTop([FromQuery] int limit = 20)
        {
            var result =
                await _service.GetTopPerformingMatchesAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("won")]
        public async Task<
            ActionResult<List<PropertyMatchSalesPerformanceItemDto>>>
            GetWon([FromQuery] int limit = 20)
        {
            var items =
                await _service.GetTopPerformingMatchesAsync(100);

            var result = items
                .Where(x =>
                    string.Equals(
                        x.Stage,
                        "Won",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.PerformanceScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("offers")]
        public async Task<
            ActionResult<List<PropertyMatchSalesPerformanceItemDto>>>
            GetOffers([FromQuery] int limit = 20)
        {
            var items =
                await _service.GetTopPerformingMatchesAsync(100);

            var result = items
                .Where(x =>
                    string.Equals(
                        x.Stage,
                        "Offer",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.PerformanceScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("high-quality")]
        public async Task<
            ActionResult<List<PropertyMatchSalesPerformanceItemDto>>>
            GetHighQuality([FromQuery] int limit = 30)
        {
            var items =
                await _service.GetTopPerformingMatchesAsync(100);

            var result = items
                .Where(x => x.MatchScore >= 80)
                .OrderByDescending(x => x.PerformanceScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("full-match")]
        public async Task<
            ActionResult<List<PropertyMatchSalesPerformanceItemDto>>>
            GetFullMatches([FromQuery] int limit = 30)
        {
            var items =
                await _service.GetTopPerformingMatchesAsync(100);

            var result = items
                .Where(x => x.MatchedCriteriaCount >= 5)
                .OrderByDescending(x => x.PerformanceScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("conversion")]
        public async Task<IActionResult> GetConversion()
        {
            var performance =
                await _service.GetPerformanceAsync();

            return Ok(new
            {
                totalMatches =
                    performance.TotalMatches,

                wonMatches =
                    performance.WonMatches,

                lostMatches =
                    performance.LostMatches,

                activeMatches =
                    performance.ActiveMatches,

                conversionRate =
                    performance.ConversionRate,

                overallConversionRate =
                    performance.OverallConversionRate,

                performanceScore =
                    performance.PerformanceScore,

                performanceLevel =
                    performance.PerformanceLevel,

                generatedAt =
                    performance.GeneratedAt
            });
        }

        [HttpGet("pipeline")]
        public async Task<IActionResult> GetPipeline()
        {
            var stages =
                await _service.GetStagePerformanceAsync();

            var result = stages
                .OrderBy(x => GetStageOrder(x.Stage))
                .ToList();

            return Ok(result);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var performanceTask =
                _service.GetPerformanceAsync();

            var stagesTask =
                _service.GetStagePerformanceAsync();

            var topTask =
                _service.GetTopPerformingMatchesAsync(10);

            await Task.WhenAll(
                performanceTask,
                stagesTask,
                topTask);

            return Ok(new
            {
                performance =
                    await performanceTask,

                pipeline =
                    await stagesTask,

                topMatches =
                    await topTask,

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
                    "PropertyCustomerMatchSalesPerformance",

                status = "Running",

                timestamp = DateTime.UtcNow
            });
        }

        private static int GetStageOrder(string stage)
        {
            return stage
                .Trim()
                .ToLowerInvariant() switch
            {
                "new" => 1,
                "viewed" => 2,
                "contacted" => 3,
                "meeting" => 4,
                "offer" => 5,
                "won" => 6,
                "lost" => 7,
                _ => 99
            };
        }

        private static int NormalizeLimit(int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(limit, 100);
        }
    }
}
