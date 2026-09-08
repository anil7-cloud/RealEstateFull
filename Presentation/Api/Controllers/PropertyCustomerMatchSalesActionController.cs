using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-action")]
    public class PropertyCustomerMatchSalesActionController : ControllerBase
    {
        private readonly PropertyCustomerMatchSalesActionService _service;

        public PropertyCustomerMatchSalesActionController(
            PropertyCustomerMatchSalesActionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyMatchSalesActionDto>>>
            GetAll([FromQuery] int limit = 50)
        {
            var result = await _service.GetActionsAsync(limit);

            return Ok(result);
        }

        [HttpGet("match/{matchId:int}")]
        public async Task<ActionResult<PropertyMatchSalesActionDto>>
            GetForMatch(int matchId)
        {
            if (matchId <= 0)
            {
                return BadRequest(new
                {
                    message = "MatchId geçerli olmalıdır."
                });
            }

            var result = await _service
                .GetActionForMatchAsync(matchId);

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
        public async Task<ActionResult<List<PropertyMatchSalesActionDto>>>
            GetForLead(
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

            var result = await _service.GetLeadActionsAsync(
                leadId,
                limit);

            return Ok(result);
        }

        [HttpGet("property/{propertyId:int}")]
        public async Task<ActionResult<List<PropertyMatchSalesActionDto>>>
            GetForProperty(
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

            var result = await _service.GetPropertyActionsAsync(
                propertyId,
                limit);

            return Ok(result);
        }

        [HttpGet("urgent")]
        public async Task<ActionResult<List<PropertyMatchSalesActionDto>>>
            GetUrgent([FromQuery] int limit = 20)
        {
            var actions = await _service.GetActionsAsync(100);

            var result = actions
                .Where(x => x.IsUrgent)
                .OrderByDescending(x => x.ActionScore)
                .ThenBy(x => x.ExecuteBefore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<List<PropertyMatchSalesActionDto>>>
            GetOverdue([FromQuery] int limit = 20)
        {
            var actions = await _service.GetActionsAsync(100);

            var now = DateTime.UtcNow;

            var result = actions
                .Where(x => x.ExecuteBefore < now)
                .OrderBy(x => x.ExecuteBefore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("type/{actionType}")]
        public async Task<ActionResult<List<PropertyMatchSalesActionDto>>>
            GetByType(
                string actionType,
                [FromQuery] int limit = 30)
        {
            if (string.IsNullOrWhiteSpace(actionType))
            {
                return BadRequest(new
                {
                    message = "ActionType gereklidir."
                });
            }

            var actions = await _service.GetActionsAsync(100);

            var result = actions
                .Where(x =>
                    string.Equals(
                        x.ActionType,
                        actionType,
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.ActionScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("today")]
        public async Task<ActionResult<List<PropertyMatchSalesActionDto>>>
            GetToday([FromQuery] int limit = 50)
        {
            var actions = await _service.GetActionsAsync(100);

            var now = DateTime.UtcNow;
            var end = now.Date.AddDays(1);

            var result = actions
                .Where(x =>
                    x.ExecuteBefore >= now &&
                    x.ExecuteBefore < end)
                .OrderBy(x => x.ExecuteBefore)
                .ThenByDescending(x => x.ActionScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var actions = await _service.GetActionsAsync(100);

            var now = DateTime.UtcNow;

            return Ok(new
            {
                total = actions.Count,

                urgent = actions.Count(x => x.IsUrgent),

                overdue = actions.Count(
                    x => x.ExecuteBefore < now),

                calls = actions.Count(
                    x => x.ActionType == "Call"),

                whatsApp = actions.Count(
                    x => x.ActionType == "SendWhatsApp"),

                emails = actions.Count(
                    x => x.ActionType == "SendEmail"),

                meetings = actions.Count(
                    x => x.ActionType == "ScheduleMeeting"),

                offerFollowUps = actions.Count(
                    x => x.ActionType == "FollowOffer"),

                averageActionScore =
                    actions.Count == 0
                        ? 0
                        : Math.Round(
                            actions.Average(
                                x => x.ActionScore),
                            2),

                generatedAt = now
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service = "PropertyCustomerMatchSalesAction",
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
