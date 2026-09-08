using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSettingsManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _settingsService;

    public PropertyAiCustomerSalesLeadForecastSettingsManagerController(
        IPropertyAiCustomerSalesLeadForecastCoreService settingsService)
    {
        _settingsService = settingsService;
    }

    [HttpGet("settings/{userId}")]
    public async Task<IActionResult> GetSettings(int userId)
    {
        var result = await _settingsService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("settings/dashboard/{userId}")]
    public async Task<IActionResult> GetSettingsDashboard(int userId)
    {
        var result = await _settingsService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
