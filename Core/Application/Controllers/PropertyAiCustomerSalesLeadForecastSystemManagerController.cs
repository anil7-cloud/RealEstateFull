using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSystemManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _systemService;

    public PropertyAiCustomerSalesLeadForecastSystemManagerController(
        IPropertyAiCustomerSalesLeadForecastCoreService systemService)
    {
        _systemService = systemService;
    }

    [HttpGet("system/dashboard/{userId}")]
    public async Task<IActionResult> GetSystemDashboard(int userId)
    {
        var result = await _systemService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("system/forecast/{userId}")]
    public async Task<IActionResult> GetSystemForecast(int userId)
    {
        var result = await _systemService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("system/metrics/{userId}")]
    public async Task<IActionResult> GetSystemMetrics(int userId)
    {
        var result = await _systemService.GetMetricsAsync(userId);

        return Ok(result);
    }
}
