using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-forecast-insight")]
    public class PropertyCustomerMatchSalesForecastInsightController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesForecastInsightService _service;

        public PropertyCustomerMatchSalesForecastInsightController(
            PropertyCustomerMatchSalesForecastInsightService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<
            ActionResult<PropertyMatchSalesForecastInsightDto>>
            GetInsight()
        {
            var result =
                await _service.GetInsightAsync();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<
            ActionResult<PropertyMatchSalesForecastInsightDto>>
            GetSummary()
        {
            var result =
                await _service.GetInsightAsync();

            return Ok(result);
        }

        [HttpGet("opportunities")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesForecastInsightItemDto>>>
            GetOpportunities(
                [FromQuery] int limit = 30)
        {
            var result =
                await _service.GetOpportunityInsightsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("urgent")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesForecastInsightItemDto>>>
            GetUrgent(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetUrgentInsightsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("weak")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesForecastInsightItemDto>>>
            GetWeak(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetWeakInsightsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("strong")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesForecastInsightItemDto>>>
            GetStrong(
                [FromQuery] int limit = 20)
        {
            var items =
                await _service
                    .GetOpportunityInsightsAsync(100);

            var result = items
                .Where(x =>
                    x.InsightLevel == "Excellent" ||
                    x.InsightLevel == "Strong")
                .OrderByDescending(
                    x => x.InsightScore)
                .Take(
                    NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("critical")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesForecastInsightItemDto>>>
            GetCritical(
                [FromQuery] int limit = 20)
        {
            var items =
                await _service
                    .GetOpportunityInsightsAsync(100);

            var result = items
                .Where(x =>
                    string.Equals(
                        x.InsightLevel,
                        "Critical",
                        StringComparison.OrdinalIgnoreCase))
                .OrderBy(
                    x => x.InsightScore)
                .Take(
                    NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("high-conversion")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesForecastInsightItemDto>>>
            GetHighConversion(
                [FromQuery] decimal minimumProbability = 75,
                [FromQuery] int limit = 20)
        {
            minimumProbability =
                Math.Clamp(
                    minimumProbability,
                    0,
                    100);

            var items =
                await _service
                    .GetOpportunityInsightsAsync(100);

            var result = items
                .Where(x =>
                    x.ConversionProbability >=
                    minimumProbability)
                .OrderByDescending(
                    x => x.ConversionProbability)
                .ThenByDescending(
                    x => x.InsightScore)
                .Take(
                    NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("offer-stage")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesForecastInsightItemDto>>>
            GetOfferStage(
                [FromQuery] int limit = 20)
        {
            var items =
                await _service
                    .GetOpportunityInsightsAsync(100);

            var result = items
                .Where(x =>
                    string.Equals(
                        x.Stage,
                        "Offer",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(
                    x => x.InsightScore)
                .Take(
                    NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("management")]
        public async Task<IActionResult>
            GetManagementInsight()
        {
            var insight =
                await _service.GetInsightAsync();

            return Ok(new
            {
                totalOpportunities =
                    insight.TotalOpportunities,

                strongOpportunities =
                    insight.StrongOpportunities,

                weakOpportunities =
                    insight.WeakOpportunities,

                urgentOpportunities =
                    insight.UrgentOpportunities,

                expectedSales =
                    insight.ExpectedSales,

                insightScore =
                    insight.InsightScore,

                insightLevel =
                    insight.InsightLevel,

                mainInsight =
                    insight.MainInsight,

                recommendation =
                    insight.ManagementRecommendation,

                warning =
                    insight.PipelineWarning,

                generatedAt =
                    insight.GeneratedAt
            });
        }

        [HttpGet("warnings")]
        public async Task<IActionResult>
            GetWarnings()
        {
            var insight =
                await _service.GetInsightAsync();

            var urgent =
                await _service.GetUrgentInsightsAsync(10);

            var weak =
                await _service.GetWeakInsightsAsync(10);

            return Ok(new
            {
                pipelineWarning =
                    insight.PipelineWarning,

                urgentOpportunityCount =
                    insight.UrgentOpportunities,

                weakOpportunityCount =
                    insight.WeakOpportunities,

                urgentOpportunities =
                    urgent,

                weakOpportunities =
                    weak,

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult>
            GetDashboard()
        {
            var insightTask =
                _service.GetInsightAsync();

            var opportunitiesTask =
                _service.GetOpportunityInsightsAsync(100);

            var urgentTask =
                _service.GetUrgentInsightsAsync(10);

            await Task.WhenAll(
                insightTask,
                opportunitiesTask,
                urgentTask);

            var insight =
                await insightTask;

            var opportunities =
                await opportunitiesTask;

            var urgent =
                await urgentTask;

            var strongest =
                opportunities
                    .OrderByDescending(
                        x => x.InsightScore)
                    .Take(10)
                    .ToList();

            var weakest =
                opportunities
                    .OrderBy(
                        x => x.InsightScore)
                    .Take(10)
                    .ToList();

            return Ok(new
            {
                insight,

                strongest,

                weakest,

                urgent,

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
                    "PropertyCustomerMatchSalesForecastInsight",

                status =
                    "Running",

                timestamp =
                    DateTime.UtcNow
            });
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
