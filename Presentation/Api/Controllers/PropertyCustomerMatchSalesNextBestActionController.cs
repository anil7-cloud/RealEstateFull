using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-next-best-action")]
    public class PropertyCustomerMatchSalesNextBestActionController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesNextBestActionService _service;

        public PropertyCustomerMatchSalesNextBestActionController(
            PropertyCustomerMatchSalesNextBestActionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<
            ActionResult<List<PropertyMatchNextBestActionDto>>>
            GetAll([FromQuery] int limit = 50)
        {
            var result =
                await _service.GetActionsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<
            ActionResult<PropertyMatchNextBestActionSummaryDto>>
            GetSummary()
        {
            var result =
                await _service.GetSummaryAsync();

            return Ok(result);
        }

        [HttpGet("match/{matchId:int}")]
        public async Task<
            ActionResult<PropertyMatchNextBestActionDto>>
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
                await _service.GetForMatchAsync(matchId);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Eşleşme için aksiyon bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}")]
        public async Task<
            ActionResult<List<PropertyMatchNextBestActionDto>>>
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
                await _service.GetForLeadAsync(
                    leadId,
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}/best")]
        public async Task<
            ActionResult<PropertyMatchNextBestActionDto>>
            GetBestForLead(int leadId)
        {
            if (leadId <= 0)
            {
                return BadRequest(new
                {
                    message = "LeadId geçerli olmalıdır."
                });
            }

            var result =
                await _service.GetBestForLeadAsync(leadId);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Lead için önerilen aksiyon bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("immediate")]
        public async Task<
            ActionResult<List<PropertyMatchNextBestActionDto>>>
            GetImmediate(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetImmediateActionsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("critical")]
        public async Task<
            ActionResult<List<PropertyMatchNextBestActionDto>>>
            GetCritical(
                [FromQuery] int limit = 20)
        {
            var actions =
                await _service.GetActionsAsync(100);

            var result = actions
                .Where(x =>
                    string.Equals(
                        x.Urgency,
                        "Critical",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.ActionScore)
                .ThenByDescending(x => x.UrgencyScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("high-urgency")]
        public async Task<
            ActionResult<List<PropertyMatchNextBestActionDto>>>
            GetHighUrgency(
                [FromQuery] int limit = 20)
        {
            var actions =
                await _service.GetActionsAsync(100);

            var result = actions
                .Where(x =>
                    x.Urgency == "Critical" ||
                    x.Urgency == "High")
                .OrderByDescending(x => x.UrgencyScore)
                .ThenByDescending(x => x.ActionScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("calls")]
        public async Task<
            ActionResult<List<PropertyMatchNextBestActionDto>>>
            GetCalls(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByActionAsync(
                    "Call",
                    limit));
        }

        [HttpGet("meetings")]
        public async Task<
            ActionResult<List<PropertyMatchNextBestActionDto>>>
            GetMeetings(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByActionAsync(
                    "ScheduleMeeting",
                    limit));
        }

        [HttpGet("offers")]
        public async Task<
            ActionResult<List<PropertyMatchNextBestActionDto>>>
            GetOffers(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByActionAsync(
                    "SendOffer",
                    limit));
        }

        [HttpGet("follow-ups")]
        public async Task<
            ActionResult<List<PropertyMatchNextBestActionDto>>>
            GetFollowUps(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByActionAsync(
                    "FollowUp",
                    limit));
        }

        [HttpGet("alternatives")]
        public async Task<
            ActionResult<List<PropertyMatchNextBestActionDto>>>
            GetAlternatives(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByActionAsync(
                    "SuggestAlternative",
                    limit));
        }

        [HttpGet("phone-channel")]
        public async Task<
            ActionResult<List<PropertyMatchNextBestActionDto>>>
            GetPhoneChannel(
                [FromQuery] int limit = 20)
        {
            var actions =
                await _service.GetActionsAsync(100);

            var result = actions
                .Where(x =>
                    x.RecommendedChannel.Contains(
                        "Phone",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.ActionScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("due-soon")]
        public async Task<
            ActionResult<List<PropertyMatchNextBestActionDto>>>
            GetDueSoon(
                [FromQuery] int hours = 4,
                [FromQuery] int limit = 20)
        {
            hours = Math.Clamp(hours, 1, 168);

            var actions =
                await _service.GetActionsAsync(100);

            var result = actions
                .Where(x =>
                    x.ExecuteWithinHours <= hours)
                .OrderBy(x => x.ExecuteWithinHours)
                .ThenByDescending(x => x.ActionScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("top")]
        public async Task<
            ActionResult<List<PropertyMatchNextBestActionDto>>>
            GetTop([FromQuery] int limit = 10)
        {
            var result =
                await _service.GetActionsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult>
            GetDashboard()
        {
            var summaryTask =
                _service.GetSummaryAsync();

            var actionsTask =
                _service.GetActionsAsync(100);

            var immediateTask =
                _service.GetImmediateActionsAsync(10);

            await Task.WhenAll(
                summaryTask,
                actionsTask,
                immediateTask);

            var summary =
                await summaryTask;

            var actions =
                await actionsTask;

            var immediate =
                await immediateTask;

            var topActions = actions
                .OrderByDescending(x => x.ActionScore)
                .Take(10)
                .ToList();

            var critical = actions
                .Where(x =>
                    x.Urgency == "Critical")
                .OrderByDescending(x => x.UrgencyScore)
                .Take(10)
                .ToList();

            var offers = actions
                .Where(x =>
                    x.NextBestAction == "SendOffer")
                .OrderByDescending(x => x.ActionScore)
                .Take(10)
                .ToList();

            return Ok(new
            {
                summary,

                immediateActions =
                    immediate,

                topActions,

                critical,

                offers,

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
                    "PropertyCustomerMatchSalesNextBestAction",

                status =
                    "Running",

                timestamp =
                    DateTime.UtcNow
            });
        }

        private async Task<List<PropertyMatchNextBestActionDto>>
            FilterByActionAsync(
                string action,
                int limit)
        {
            var actions =
                await _service.GetActionsAsync(100);

            return actions
                .Where(x =>
                    string.Equals(
                        x.NextBestAction,
                        action,
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.ActionScore)
                .ThenByDescending(x => x.UrgencyScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        private static int NormalizeLimit(
            int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(limit, 100);
        }
    }
}
