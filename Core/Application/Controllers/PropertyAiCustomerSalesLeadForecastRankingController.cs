using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastRankingController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _rankingService;

    public PropertyAiCustomerSalesLeadForecastRankingController(
        IPropertyAiCustomerSalesLeadForecastCoreService rankingService)
    {
        _rankingService = rankingService;
    }

    [HttpGet("ranking/{userId}")]
    public async Task<IActionResult> GetRanking(int userId)
    {
        var result = await _rankingService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("ranking/dashboard/{userId}")]
    public async Task<IActionResult> GetRankingDashboard(int userId)
    {
        var result = await _rankingService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
