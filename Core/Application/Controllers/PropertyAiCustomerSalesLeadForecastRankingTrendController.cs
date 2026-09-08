using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastRankingTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _rankingTrendService;

    public PropertyAiCustomerSalesLeadForecastRankingTrendController(
        IPropertyAiCustomerSalesLeadForecastService rankingTrendService)
    {
        _rankingTrendService = rankingTrendService;
    }

    [HttpGet("ranking-trend/{userId}")]
    public async Task<IActionResult> GetRankingTrend(int userId)
    {
        var result = await _rankingTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("ranking-trend/history/{userId}")]
    public async Task<IActionResult> GetRankingTrendHistory(int userId)
    {
        var result = await _rankingTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
