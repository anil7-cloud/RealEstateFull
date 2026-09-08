using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-conversion")]
    public class PropertyCustomerMatchConversionController : ControllerBase
    {
        private readonly PropertyCustomerMatchConversionService _service;

        public PropertyCustomerMatchConversionController(
            PropertyCustomerMatchConversionService service)
        {
            _service = service;
        }

        [HttpGet("analytics")]
        public async Task<ActionResult<PropertyMatchConversionAnalyticsDto>>
            GetAnalytics()
        {
            var result = await _service.GetAnalyticsAsync();

            return Ok(result);
        }

        [HttpGet("stages")]
        public IActionResult GetStages()
        {
            return Ok(_service.GetValidStages());
        }

        [HttpGet("stage/{stage}")]
        public async Task<ActionResult<List<PropertyCustomerMatchDto>>>
            GetByStage(string stage)
        {
            try
            {
                var result = await _service.GetByStageAsync(stage);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("{matchId:int}/stage")]
        public async Task<ActionResult<PropertyCustomerMatchDto>>
            ChangeStage(
                int matchId,
                [FromBody] PropertyMatchStageRequest request)
        {
            if (matchId <= 0)
            {
                return BadRequest(new
                {
                    message = "MatchId geçerli olmalıdır."
                });
            }

            if (string.IsNullOrWhiteSpace(request.Stage))
            {
                return BadRequest(new
                {
                    message = "Stage gereklidir."
                });
            }

            try
            {
                var result = await _service.ChangeStageAsync(
                    matchId,
                    request.Stage);

                if (result == null)
                {
                    return NotFound(new
                    {
                        message = "Eşleşme bulunamadı."
                    });
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("funnel")]
        public async Task<IActionResult> GetFunnel()
        {
            var analytics = await _service.GetAnalyticsAsync();

            return Ok(new
            {
                total = analytics.TotalMatches,
                @new = analytics.NewCount,
                viewed = analytics.ViewedCount,
                contacted = analytics.ContactedCount,
                meeting = analytics.MeetingCount,
                offer = analytics.OfferCount,
                won = analytics.WonCount,
                lost = analytics.LostCount
            });
        }

        [HttpGet("rates")]
        public async Task<IActionResult> GetRates()
        {
            var analytics = await _service.GetAnalyticsAsync();

            return Ok(new
            {
                contactRate = analytics.ContactRate,
                meetingRate = analytics.MeetingRate,
                offerRate = analytics.OfferRate,
                winRate = analytics.WinRate,
                overallConversionRate =
                    analytics.OverallConversionRate
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service = "PropertyCustomerMatchConversion",
                status = "Running",
                timestamp = DateTime.UtcNow
            });
        }
    }

    public class PropertyMatchStageRequest
    {
        public string Stage { get; set; } = string.Empty;
    }
}
