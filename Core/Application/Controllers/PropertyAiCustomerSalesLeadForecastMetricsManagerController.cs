using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastMetricsManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastMetricsService _metricsService;

    public PropertyAiCustomerSalesLeadForecastMetricsManagerController(
        IPropertyAiCustomerSalesLeadForecastMetricsService metricsService)
    {
        _metricsService = metricsService;
    }

    [HttpGet("metrics/{userId}")]
    public async Task<IActionResult> GetMetrics(int userId)
    {
        var result = await _metricsService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("metrics/history/{userId}")]
    public async Task<IActionResult> GetMetricsHistory(int userId)
    {
        var result = await _metricsService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
