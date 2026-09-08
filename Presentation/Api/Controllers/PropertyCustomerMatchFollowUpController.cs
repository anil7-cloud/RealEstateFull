using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-follow-up")]
    public class PropertyCustomerMatchFollowUpController : ControllerBase
    {
        private readonly PropertyCustomerMatchFollowUpService _service;

        public PropertyCustomerMatchFollowUpController(
            PropertyCustomerMatchFollowUpService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyMatchFollowUpDto>>>
            GetAll(
                [FromQuery] decimal minimumScore = 60,
                [FromQuery] int limit = 50)
        {
            var result = await _service.GetFollowUpsAsync(
                minimumScore,
                limit);

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}")]
        public async Task<ActionResult<List<PropertyMatchFollowUpDto>>>
            GetForLead(
                int leadId,
                [FromQuery] int limit = 20)
        {
            if (leadId <= 0)
                return BadRequest("LeadId geçerli olmalıdır.");

            var result = await _service.GetLeadFollowUpsAsync(
                leadId,
                limit);

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}/next")]
        public async Task<ActionResult<PropertyMatchFollowUpDto>>
            GetNextForLead(int leadId)
        {
            if (leadId <= 0)
                return BadRequest("LeadId geçerli olmalıdır.");

            var result = await _service.GetNextFollowUpAsync(
                leadId);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Bu lead için aktif takip bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("urgent")]
        public async Task<ActionResult<List<PropertyMatchFollowUpDto>>>
            GetUrgent([FromQuery] int limit = 20)
        {
            var followUps = await _service.GetFollowUpsAsync(
                75,
                100);

            var now = DateTime.UtcNow;

            var result = followUps
                .Where(x =>
                    x.FollowUpAt <= now ||
                    x.Priority == "Critical" ||
                    x.Priority == "VeryHigh")
                .OrderBy(x => x.FollowUpAt)
                .ThenByDescending(x => x.MatchScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<List<PropertyMatchFollowUpDto>>>
            GetOverdue([FromQuery] int limit = 50)
        {
            var followUps = await _service.GetFollowUpsAsync(
                0,
                100);

            var now = DateTime.UtcNow;

            var result = followUps
                .Where(x => x.FollowUpAt < now)
                .OrderBy(x => x.FollowUpAt)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("today")]
        public async Task<ActionResult<List<PropertyMatchFollowUpDto>>>
            GetToday([FromQuery] int limit = 50)
        {
            var followUps = await _service.GetFollowUpsAsync(
                0,
                100);

            var now = DateTime.UtcNow;
            var end = now.Date.AddDays(1);

            var result = followUps
                .Where(x =>
                    x.FollowUpAt >= now &&
                    x.FollowUpAt < end)
                .OrderBy(x => x.FollowUpAt)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var followUps = await _service.GetFollowUpsAsync(
                0,
                100);

            var now = DateTime.UtcNow;
            var end = now.Date.AddDays(1);

            return Ok(new
            {
                total = followUps.Count,

                overdue = followUps.Count(
                    x => x.FollowUpAt < now),

                dueToday = followUps.Count(
                    x =>
                        x.FollowUpAt >= now &&
                        x.FollowUpAt < end),

                critical = followUps.Count(
                    x => x.Priority == "Critical"),

                veryHigh = followUps.Count(
                    x => x.Priority == "VeryHigh"),

                high = followUps.Count(
                    x => x.Priority == "High"),

                generatedAt = now
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service = "PropertyCustomerMatchFollowUp",
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
