using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastOverviewManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _overviewService;

    public PropertyAiCustomerSalesLeadForecastOverviewManagerController(
        IPropertyAiCustomerSalesLeadForecastCoreService overviewService)
    {
        _overviewService = overviewService;
    }

    [HttpGet("overview/{userId}")]
    public async Task<IActionResult> GetOverview(int userId)
    {
        var result = await _overviewService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("overview/forecast/{userId}")]
    public async Task<IActionResult> GetOverviewForecast(int userId)
    {
        var result = await _overviewService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("overview/metrics/{userId}")]
    public async Task<IActionResult> GetOverviewMetrics(int userId)
    {
        var result = await _overviewService.GetMetricsAsync(userId);

        return Ok(result);
    }
}
