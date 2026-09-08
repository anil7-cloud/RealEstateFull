using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSystemController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _systemService;

    public PropertyAiCustomerSalesLeadForecastSystemController(
        IPropertyAiCustomerSalesLeadForecastCoreService systemService)
    {
        _systemService = systemService;
    }

    [HttpGet("system/status/{userId}")]
    public async Task<IActionResult> GetSystemStatus(int userId)
    {
        var result = await _systemService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("system/dashboard/{userId}")]
    public async Task<IActionResult> GetSystemDashboard(int userId)
    {
        var result = await _systemService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
