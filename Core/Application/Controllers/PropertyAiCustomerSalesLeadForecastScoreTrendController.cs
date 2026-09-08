using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastScoreTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _scoreTrendService;

    public PropertyAiCustomerSalesLeadForecastScoreTrendController(
        IPropertyAiCustomerSalesLeadForecastService scoreTrendService)
    {
        _scoreTrendService = scoreTrendService;
    }

    [HttpGet("score-trend/{userId}")]
    public async Task<IActionResult> GetScoreTrend(int userId)
    {
        var result = await _scoreTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("score-trend/history/{userId}")]
    public async Task<IActionResult> GetScoreTrendHistory(int userId)
    {
        var result = await _scoreTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
