using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastAnalyticsHistoryController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastAnalyticsService _analyticsService;

    public PropertyAiCustomerSalesLeadForecastAnalyticsHistoryController(
        IPropertyAiCustomerSalesLeadForecastAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("analytics-history/{userId}")]
    public async Task<IActionResult> GetAnalyticsHistory(int userId)
    {
        var result = await _analyticsService.GetHistoryAsync(userId);

        return Ok(result);
    }

    [HttpGet("analytics/{userId}")]
    public async Task<IActionResult> GetAnalytics(int userId)
    {
        var result = await _analyticsService.GetAnalyticsAsync(userId);

        return Ok(result);
    }
}
