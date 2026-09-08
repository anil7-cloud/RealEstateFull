using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastHealthController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _healthService;

    public PropertyAiCustomerSalesLeadForecastHealthController(
        IPropertyAiCustomerSalesLeadForecastCoreService healthService)
    {
        _healthService = healthService;
    }

    [HttpGet("health/{userId}")]
    public async Task<IActionResult> GetHealth(int userId)
    {
        var result = await _healthService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("health/dashboard/{userId}")]
    public async Task<IActionResult> GetHealthDashboard(int userId)
    {
        var result = await _healthService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
