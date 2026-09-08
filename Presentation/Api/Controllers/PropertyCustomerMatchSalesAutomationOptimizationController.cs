using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation-optimization")]
    public class PropertyCustomerMatchSalesAutomationOptimizationController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOptimizationService _service;

        public PropertyCustomerMatchSalesAutomationOptimizationController(
            PropertyCustomerMatchSalesAutomationOptimizationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationOptimizationDto>>
            GetOptimization()
        {
            var result =
                await _service.GetOptimizationAsync();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult>
            GetSummary()
        {
            var result =
                await _service.GetOptimizationAsync();

            return Ok(new
            {
                currentHealthScore =
                    result.CurrentHealthScore,

                currentSuccessRate =
                    result.CurrentSuccessRate,

                currentFailureRate =
                    result.CurrentFailureRate,

                overdueCount =
                    result.OverdueCount,

                optimizationScore =
                    result.OptimizationScore,

                optimizationLevel =
                    result.OptimizationLevel,

                recommendedChanges =
                    result.RecommendedChanges,

                highPriorityChanges =
                    result.HighPriorityChanges,

                executiveRecommendation =
                    result.ExecutiveRecommendation,

                generatedAt =
                    result.GeneratedAt
            });
        }

        [HttpGet("recommendations")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationOptimizationItemDto>>>
            GetRecommendations(
                [FromQuery] int limit = 30)
        {
            var result =
                await _service.GetRecommendationsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("critical")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationOptimizationItemDto>>>
            GetCritical(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetCriticalRecommendationsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("channels")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationOptimizationItemDto>>>
            GetChannels(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetChannelRecommendationsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("timing")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationOptimizationItemDto>>>
            GetTiming(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetTimingRecommendationsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("high-priority")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationOptimizationItemDto>>>
            GetHighPriority(
                [FromQuery] int limit = 30)
        {
            var recommendations =
                await _service.GetRecommendationsAsync(100);

            var result =
                recommendations
                    .Where(x =>
                        string.Equals(
                            x.Priority,
                            "Critical",
                            StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(
                            x.Priority,
                            "High",
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => PriorityOrder(x.Priority))
                    .ThenByDescending(
                        x => x.OptimizationImpact)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("actions")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationOptimizationItemDto>>>
            GetActionOptimizations(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByTypeAsync(
                    "Action",
                    limit));
        }

        [HttpGet("priorities")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationOptimizationItemDto>>>
            GetPriorityOptimizations(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByTypeAsync(
                    "Priority",
                    limit));
        }

        [HttpGet("retry")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationOptimizationItemDto>>>
            GetRetryOptimizations(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByTypeAsync(
                    "Retry",
                    limit));
        }

        [HttpGet("global")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationOptimizationItemDto>>>
            GetGlobalOptimizations(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByTypeAsync(
                    "Global",
                    limit));
        }

        [HttpGet("highest-impact")]
        public async Task<IActionResult>
            GetHighestImpact()
        {
            var recommendations =
                await _service.GetRecommendationsAsync(100);

            var result =
                recommendations
                    .OrderByDescending(
                        x => x.OptimizationImpact)
                    .FirstOrDefault();

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Optimizasyon önerisi bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("health")]
        public async Task<IActionResult>
            GetHealth()
        {
            var optimization =
                await _service.GetOptimizationAsync();

            return Ok(new
            {
                currentHealthScore =
                    optimization.CurrentHealthScore,

                optimizationScore =
                    optimization.OptimizationScore,

                optimizationLevel =
                    optimization.OptimizationLevel,

                successRate =
                    optimization.CurrentSuccessRate,

                failureRate =
                    optimization.CurrentFailureRate,

                overdue =
                    optimization.OverdueCount,

                generatedAt =
                    optimization.GeneratedAt
            });
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult>
            GetDashboard()
        {
            var optimizationTask =
                _service.GetOptimizationAsync();

            var recommendationsTask =
                _service.GetRecommendationsAsync(100);

            var criticalTask =
                _service.GetCriticalRecommendationsAsync(10);

            var channelsTask =
                _service.GetChannelRecommendationsAsync(10);

            var timingTask =
                _service.GetTimingRecommendationsAsync(10);

            await Task.WhenAll(
                optimizationTask,
                recommendationsTask,
                criticalTask,
                channelsTask,
                timingTask);

            var optimization =
                await optimizationTask;

            var recommendations =
                await recommendationsTask;

            var critical =
                await criticalTask;

            var channels =
                await channelsTask;

            var timing =
                await timingTask;

            var highestImpact =
                recommendations
                    .OrderByDescending(
                        x => x.OptimizationImpact)
                    .Take(10)
                    .ToList();

            return Ok(new
            {
                optimization,

                critical,

                channelOptimizations =
                    channels,

                timingOptimizations =
                    timing,

                highestImpact,

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("service-health")]
        public IActionResult ServiceHealth()
        {
            return Ok(new
            {
                service =
                    "PropertyCustomerMatchSalesAutomationOptimization",

                status =
                    "Running",

                timestamp =
                    DateTime.UtcNow
            });
        }

        private async Task<
            List<PropertyMatchSalesAutomationOptimizationItemDto>>
            FilterByTypeAsync(
                string type,
                int limit)
        {
            var recommendations =
                await _service.GetRecommendationsAsync(100);

            return recommendations
                .Where(x =>
                    string.Equals(
                        x.OptimizationType,
                        type,
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(
                    x => x.OptimizationImpact)
                .Take(
                    NormalizeLimit(limit))
                .ToList();
        }

        private static int PriorityOrder(
            string priority)
        {
            return priority switch
            {
                "Critical" => 4,
                "High" => 3,
                "Medium" => 2,
                "Low" => 1,
                _ => 0
            };
        }

        private static int NormalizeLimit(
            int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(
                limit,
                100);
        }
    }
}
