using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-dashboard")]
    public class PropertyCustomerMatchDashboardController : ControllerBase
    {
        private readonly PropertyCustomerMatchDashboardService _dashboardService;

        public PropertyCustomerMatchDashboardController(
            PropertyCustomerMatchDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<ActionResult<PropertyCustomerMatchDashboardDto>>
            GetDashboard()
        {
            var result = await _dashboardService.GetDashboardAsync();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var result = await _dashboardService.GetDashboardAsync();

            return Ok(new
            {
                result.TotalMatches,
                result.AverageMatchScore,
                result.HotLeads,
                result.MatchedProperties,
                result.MatchedLeads,
                result.BestMatchScore
            });
        }

        [HttpGet("distribution")]
        public async Task<IActionResult> GetDistribution()
        {
            var result = await _dashboardService.GetDashboardAsync();

            return Ok(new
            {
                excellent = result.ExcellentMatches,
                high = result.HighPotentialMatches,
                medium = result.MediumMatches,
                low = result.LowMatches
            });
        }

        [HttpGet("best-match")]
        public async Task<IActionResult> GetBestMatch()
        {
            var result = await _dashboardService.GetDashboardAsync();

            if (result.BestMatchLeadId == null ||
                result.BestMatchPropertyId == null)
            {
                return NotFound(new
                {
                    message = "Henüz eşleşme bulunamadı."
                });
            }

            return Ok(new
            {
                propertyId = result.BestMatchPropertyId,
                leadId = result.BestMatchLeadId,
                score = result.BestMatchScore
            });
        }
    }
}
