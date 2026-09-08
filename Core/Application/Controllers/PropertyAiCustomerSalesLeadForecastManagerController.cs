using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _managerService;

    public PropertyAiCustomerSalesLeadForecastManagerController(
        IPropertyAiCustomerSalesLeadForecastCoreService managerService)
    {
        _managerService = managerService;
    }

    [HttpGet("manager/dashboard/{userId}")]
    public async Task<IActionResult> GetManagerDashboard(int userId)
    {
        var result = await _managerService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("manager/forecast/{userId}")]
    public async Task<IActionResult> GetManagerForecast(int userId)
    {
        var result = await _managerService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("manager/metrics/{userId}")]
    public async Task<IActionResult> GetManagerMetrics(int userId)
    {
        var result = await _managerService.GetMetricsAsync(userId);

        return Ok(result);
    }
}
