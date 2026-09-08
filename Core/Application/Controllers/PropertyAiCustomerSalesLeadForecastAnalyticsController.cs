using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastAnalyticsController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastAnalyticsService _analyticsService;

    public PropertyAiCustomerSalesLeadForecastAnalyticsController(
        IPropertyAiCustomerSalesLeadForecastAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("analytics/{userId}")]
    public async Task<IActionResult> GetAnalytics(int userId)
    {
        var result = await _analyticsService.GetAnalyticsAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _analyticsService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
