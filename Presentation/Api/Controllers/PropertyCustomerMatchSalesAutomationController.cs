using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation")]
    public class PropertyCustomerMatchSalesAutomationController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesAutomationService _service;

        public PropertyCustomerMatchSalesAutomationController(
            PropertyCustomerMatchSalesAutomationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
            GetAll([FromQuery] int limit = 50)
        {
            var result =
                await _service.GetAutomationsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationSummaryDto>>
            GetSummary()
        {
            var result =
                await _service.GetSummaryAsync();

            return Ok(result);
        }

        [HttpGet("match/{matchId:int}")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationDto>>
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
                    message =
                        "Eşleşme için otomasyon bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
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

        [HttpGet("ready")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
            GetReady(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetReadyAutomationsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("urgent")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
            GetUrgent(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetUrgentAutomationsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("due-within")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
            GetDueWithin(
                [FromQuery] int hours = 24,
                [FromQuery] int limit = 20)
        {
            hours =
                Math.Clamp(
                    hours,
                    1,
                    168);

            var result =
                await _service.GetDueWithinAsync(
                    hours,
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("calls")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
            GetCalls(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByTypeAsync(
                    "Call",
                    limit));
        }

        [HttpGet("meetings")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
            GetMeetings(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByTypeAsync(
                    "Meeting",
                    limit));
        }

        [HttpGet("offers")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
            GetOffers(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByTypeAsync(
                    "Offer",
                    limit));
        }

        [HttpGet("follow-ups")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
            GetFollowUps(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByTypeAsync(
                    "FollowUp",
                    limit));
        }

        [HttpGet("alternatives")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
            GetAlternatives(
                [FromQuery] int limit = 20)
        {
            return Ok(
                await FilterByTypeAsync(
                    "AlternativeProperty",
                    limit));
        }

        [HttpGet("auto-executable")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
            GetAutoExecutable(
                [FromQuery] int limit = 30)
        {
            var automations =
                await _service.GetAutomationsAsync(100);

            var result = automations
                .Where(x => x.AutoExecutable)
                .OrderByDescending(
                    x => x.AutomationScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("approval-required")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
            GetApprovalRequired(
                [FromQuery] int limit = 30)
        {
            var automations =
                await _service.GetAutomationsAsync(100);

            var result = automations
                .Where(x => x.RequiresApproval)
                .OrderByDescending(
                    x => x.AutomationScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("scheduled")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
            GetScheduled(
                [FromQuery] int limit = 30)
        {
            var automations =
                await _service.GetAutomationsAsync(100);

            var result = automations
                .Where(x =>
                    string.Equals(
                        x.Status,
                        "Scheduled",
                        StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.ExecuteAt)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("top")]
        public async Task<
            ActionResult<List<PropertyMatchSalesAutomationDto>>>
            GetTop(
                [FromQuery] int limit = 10)
        {
            var result =
                await _service.GetAutomationsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult>
            GetDashboard()
        {
            var summaryTask =
                _service.GetSummaryAsync();

            var automationsTask =
                _service.GetAutomationsAsync(100);

            var urgentTask =
                _service.GetUrgentAutomationsAsync(10);

            var readyTask =
                _service.GetReadyAutomationsAsync(10);

            await Task.WhenAll(
                summaryTask,
                automationsTask,
                urgentTask,
                readyTask);

            var summary =
                await summaryTask;

            var automations =
                await automationsTask;

            var urgent =
                await urgentTask;

            var ready =
                await readyTask;

            var nextToExecute = automations
                .OrderBy(x => x.ExecuteAt)
                .Take(10)
                .ToList();

            var highestScore = automations
                .OrderByDescending(
                    x => x.AutomationScore)
                .Take(10)
                .ToList();

            return Ok(new
            {
                summary,

                urgent,

                ready,

                nextToExecute,

                highestScore,

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
                    "PropertyCustomerMatchSalesAutomation",

                status =
                    "Running",

                timestamp =
                    DateTime.UtcNow
            });
        }

        private async Task<
            List<PropertyMatchSalesAutomationDto>>
            FilterByTypeAsync(
                string automationType,
                int limit)
        {
            var automations =
                await _service.GetAutomationsAsync(100);

            return automations
                .Where(x =>
                    string.Equals(
                        x.AutomationType,
                        automationType,
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(
                    x => x.AutomationScore)
                .ThenBy(x => x.ExecuteAt)
                .Take(NormalizeLimit(limit))
                .ToList();
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
