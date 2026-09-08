using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastMonitoringController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _monitoringService;

    public PropertyAiCustomerSalesLeadForecastMonitoringController(
        IPropertyAiCustomerSalesLeadForecastService monitoringService)
    {
        _monitoringService = monitoringService;
    }

    [HttpGet("monitoring/{userId}")]
    public async Task<IActionResult> GetMonitoring(int userId)
    {
        var result = await _monitoringService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("status/{userId}")]
    public async Task<IActionResult> GetStatus(int userId)
    {
        var result = await _monitoringService.GetForecastAsync(userId);

        return Ok(result);
    }
}
