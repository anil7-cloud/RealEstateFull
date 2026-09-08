using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-recommendation")]
    public class PropertyCustomerMatchSalesRecommendationController
        : ControllerBase
    {
        private readonly PropertyCustomerMatchSalesRecommendationService _service;

        public PropertyCustomerMatchSalesRecommendationController(
            PropertyCustomerMatchSalesRecommendationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyMatchSalesRecommendationDto>>>
            GetAll([FromQuery] int limit = 50)
        {
            var result =
                await _service.GetRecommendationsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<PropertyMatchSalesRecommendationSummaryDto>>
            GetSummary()
        {
            var result =
                await _service.GetSummaryAsync();

            return Ok(result);
        }

        [HttpGet("match/{matchId:int}")]
        public async Task<ActionResult<PropertyMatchSalesRecommendationDto>>
            GetMatch(int matchId)
        {
            if (matchId <= 0)
            {
                return BadRequest(new
                {
                    message = "MatchId geçerli olmalıdır."
                });
            }

            var result =
                await _service.GetMatchRecommendationAsync(matchId);

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
        public async Task<ActionResult<List<PropertyMatchSalesRecommendationDto>>>
            GetLead(
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

            var result =
                await _service.GetLeadRecommendationsAsync(
                    leadId,
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("property/{propertyId:int}")]
        public async Task<ActionResult<List<PropertyMatchSalesRecommendationDto>>>
            GetProperty(
                int propertyId,
                [FromQuery] int limit = 20)
        {
            if (propertyId <= 0)
            {
                return BadRequest(new
                {
                    message = "PropertyId geçerli olmalıdır."
                });
            }

            var result =
                await _service.GetPropertyRecommendationsAsync(
                    propertyId,
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("best")]
        public async Task<ActionResult<PropertyMatchSalesRecommendationDto>>
            GetBest()
        {
            var recommendations =
                await _service.GetRecommendationsAsync(1);

            var best =
                recommendations.FirstOrDefault();

            if (best == null)
            {
                return NotFound(new
                {
                    message = "Satış önerisi bulunamadı."
                });
            }

            return Ok(best);
        }

        [HttpGet("immediate-actions")]
        public async Task<ActionResult<List<PropertyMatchSalesRecommendationDto>>>
            GetImmediateActions(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetImmediateActionsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("critical")]
        public async Task<ActionResult<List<PropertyMatchSalesRecommendationDto>>>
            GetCritical(
                [FromQuery] int limit = 20)
        {
            var items =
                await _service.GetRecommendationsAsync(100);

            var result = items
                .Where(x =>
                    string.Equals(
                        x.Priority,
                        "Critical",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.PriorityScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("high-priority")]
        public async Task<ActionResult<List<PropertyMatchSalesRecommendationDto>>>
            GetHighPriority(
                [FromQuery] int limit = 30)
        {
            var items =
                await _service.GetRecommendationsAsync(100);

            var result = items
                .Where(x =>
                    string.Equals(
                        x.Priority,
                        "Critical",
                        StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(
                        x.Priority,
                        "High",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.PriorityScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("calls")]
        public async Task<ActionResult<List<PropertyMatchSalesRecommendationDto>>>
            GetCalls(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByAction(
                    "Call",
                    limit));
        }

        [HttpGet("meetings")]
        public async Task<ActionResult<List<PropertyMatchSalesRecommendationDto>>>
            GetMeetings(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByAction(
                    "ScheduleMeeting",
                    limit));
        }

        [HttpGet("offers")]
        public async Task<ActionResult<List<PropertyMatchSalesRecommendationDto>>>
            GetOffers(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByAction(
                    "SendOffer",
                    limit));
        }

        [HttpGet("follow-ups")]
        public async Task<ActionResult<List<PropertyMatchSalesRecommendationDto>>>
            GetFollowUps(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByAction(
                    "FollowUp",
                    limit));
        }

        [HttpGet("alternatives")]
        public async Task<ActionResult<List<PropertyMatchSalesRecommendationDto>>>
            GetAlternatives(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByAction(
                    "SuggestAlternative",
                    limit));
        }

        [HttpGet("wait")]
        public async Task<ActionResult<List<PropertyMatchSalesRecommendationDto>>>
            GetWaitActions(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByAction(
                    "Wait",
                    limit));
        }

        [HttpGet("top")]
        public async Task<ActionResult<List<PropertyMatchSalesRecommendationDto>>>
            GetTop(
                [FromQuery] int limit = 10)
        {
            var result =
                await _service.GetRecommendationsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("action-counts")]
        public async Task<IActionResult>
            GetActionCounts()
        {
            var summary =
                await _service.GetSummaryAsync();

            return Ok(new
            {
                total =
                    summary.TotalRecommendations,

                immediate =
                    summary.ImmediateActions,

                calls =
                    summary.CallActions,

                meetings =
                    summary.MeetingActions,

                offers =
                    summary.OfferActions,

                alternatives =
                    summary.AlternativePropertyActions,

                followUps =
                    summary.FollowUpActions,

                waits =
                    summary.WaitActions,

                averagePriorityScore =
                    summary.AveragePriorityScore,

                generatedAt =
                    summary.GeneratedAt
            });
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult>
            GetDashboard()
        {
            var summaryTask =
                _service.GetSummaryAsync();

            var recommendationsTask =
                _service.GetRecommendationsAsync(100);

            var immediateTask =
                _service.GetImmediateActionsAsync(10);

            await Task.WhenAll(
                summaryTask,
                recommendationsTask,
                immediateTask);

            var summary =
                await summaryTask;

            var recommendations =
                await recommendationsTask;

            var immediate =
                await immediateTask;

            var topPriority = recommendations
                .OrderByDescending(x => x.PriorityScore)
                .Take(10)
                .ToList();

            var offers = recommendations
                .Where(x =>
                    string.Equals(
                        x.Action,
                        "SendOffer",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.PriorityScore)
                .Take(10)
                .ToList();

            var meetings = recommendations
                .Where(x =>
                    string.Equals(
                        x.Action,
                        "ScheduleMeeting",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.PriorityScore)
                .Take(10)
                .ToList();

            return Ok(new
            {
                summary,
                immediateActions = immediate,
                topPriority,
                offers,
                meetings,
                generatedAt = DateTime.UtcNow
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service =
                    "PropertyCustomerMatchSalesRecommendation",

                status = "Running",

                timestamp = DateTime.UtcNow
            });
        }

        private async Task<List<PropertyMatchSalesRecommendationDto>>
            FilterByAction(
                string action,
                int limit)
        {
            var recommendations =
                await _service.GetRecommendationsAsync(100);

            return recommendations
                .Where(x =>
                    string.Equals(
                        x.Action,
                        action,
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.PriorityScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        private static int NormalizeLimit(int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(limit, 100);
        }
    }
}
