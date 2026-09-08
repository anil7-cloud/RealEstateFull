using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-notification")]
    public class PropertyCustomerMatchNotificationController : ControllerBase
    {
        private readonly PropertyCustomerMatchNotificationService
            _notificationService;

        private readonly IPropertyCustomerMatchService
            _matchService;

        public PropertyCustomerMatchNotificationController(
            PropertyCustomerMatchNotificationService notificationService,
            IPropertyCustomerMatchService matchService)
        {
            _notificationService = notificationService;
            _matchService = matchService;
        }

        [HttpGet("match/{matchId:int}")]
        public async Task<IActionResult> GetForMatch(int matchId)
        {
            if (matchId <= 0)
                return BadRequest("MatchId geçerli olmalıdır.");

            var match = await _matchService.GetByIdAsync(matchId);

            if (match == null)
                return NotFound("Eşleşme bulunamadı.");

            var notification =
                _notificationService
                    .CreateHighScoreNotification(match);

            if (notification == null)
            {
                return Ok(new
                {
                    hasNotification = false,
                    message =
                        "Eşleşme skoru bildirim eşiğinin altında.",
                    matchScore = match.MatchScore
                });
            }

            return Ok(new
            {
                hasNotification = true,
                notification
            });
        }

        [HttpGet("hot-lead/{matchId:int}")]
        public async Task<IActionResult> GetHotLeadNotification(
            int matchId)
        {
            if (matchId <= 0)
                return BadRequest("MatchId geçerli olmalıdır.");

            var match = await _matchService.GetByIdAsync(matchId);

            if (match == null)
                return NotFound("Eşleşme bulunamadı.");

            var notification =
                _notificationService
                    .CreateHotLeadNotification(match);

            if (notification == null)
            {
                return Ok(new
                {
                    isHotLead = false,
                    matchScore = match.MatchScore
                });
            }

            return Ok(new
            {
                isHotLead = true,
                notification
            });
        }

        [HttpGet("high-score")]
        public async Task<
            ActionResult<List<PropertyMatchNotificationDto>>>
            GetHighScoreNotifications(
                [FromQuery] decimal minimumScore = 75)
        {
            if (minimumScore < 0)
                minimumScore = 0;

            if (minimumScore > 100)
                minimumScore = 100;

            var matches = await _matchService.GetAllAsync();

            var filtered = matches
                .Where(x => x.MatchScore >= minimumScore)
                .OrderByDescending(x => x.MatchScore)
                .ToList();

            var notifications =
                _notificationService
                    .CreateBatchNotifications(filtered);

            return Ok(notifications);
        }

        [HttpPost("stage-change")]
        public async Task<IActionResult> CreateStageChangeNotification(
            [FromBody] PropertyMatchStageNotificationRequest request)
        {
            if (request.MatchId <= 0)
                return BadRequest("MatchId geçerli olmalıdır.");

            if (string.IsNullOrWhiteSpace(request.NewStage))
                return BadRequest("NewStage gereklidir.");

            var match = await _matchService
                .GetByIdAsync(request.MatchId);

            if (match == null)
                return NotFound("Eşleşme bulunamadı.");

            var notification =
                _notificationService
                    .CreateStageChangeNotification(
                        match,
                        request.PreviousStage,
                        request.NewStage);

            return Ok(notification);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var matches = await _matchService.GetAllAsync();

            var highScoreCount =
                matches.Count(x => x.MatchScore >= 75);

            var hotLeadCount =
                matches.Count(x => x.MatchScore >= 90);

            var criticalCount =
                matches.Count(x => x.MatchScore >= 90);

            return Ok(new
            {
                totalMatches = matches.Count,
                highScoreNotifications = highScoreCount,
                hotLeads = hotLeadCount,
                criticalNotifications = criticalCount,
                generatedAt = DateTime.UtcNow
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service =
                    "PropertyCustomerMatchNotification",
                status = "Running",
                timestamp = DateTime.UtcNow
            });
        }
    }

    public class PropertyMatchStageNotificationRequest
    {
        public int MatchId { get; set; }

        public string PreviousStage { get; set; }
            = string.Empty;

        public string NewStage { get; set; }
            = string.Empty;
    }
}
