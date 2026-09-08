using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastInsightTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _insightTrendService;

    public PropertyAiCustomerSalesLeadForecastInsightTrendController(
        IPropertyAiCustomerSalesLeadForecastService insightTrendService)
    {
        _insightTrendService = insightTrendService;
    }

    [HttpGet("insight-trend/{userId}")]
    public async Task<IActionResult> GetInsightTrend(int userId)
    {
        var result = await _insightTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("insight-trend/history/{userId}")]
    public async Task<IActionResult> GetInsightTrendHistory(int userId)
    {
        var result = await _insightTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
