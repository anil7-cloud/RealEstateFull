using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _forecastService;

    public PropertyAiCustomerSalesLeadForecastController(
        IPropertyAiCustomerSalesLeadForecastCoreService forecastService)
    {
        _forecastService = forecastService;
    }

    [HttpGet("forecast/{userId}")]
    public async Task<IActionResult> GetForecast(int userId)
    {
        var result = await _forecastService.GetForecastAsync(userId);
        return Ok(result);
    }

    [HttpGet("analysis/{userId}")]
    public async Task<IActionResult> GetAnalysis(int userId)
    {
        var result = await _forecastService.GetAnalysisAsync(userId);
        return Ok(result);
    }

    [HttpGet("dashboard/{userId}")]
    public async Task<IActionResult> GetDashboard(int userId)
    {
        var result = await _forecastService.GetDashboardAsync(userId);
        return Ok(result);
    }

    [HttpGet("metrics/{userId}")]
    public async Task<IActionResult> GetMetrics(int userId)
    {
        var result = await _forecastService.GetMetricsAsync(userId);
        return Ok(result);
    }
}
