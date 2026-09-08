using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastIntegrationManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _integrationService;

    public PropertyAiCustomerSalesLeadForecastIntegrationManagerController(
        IPropertyAiCustomerSalesLeadForecastCoreService integrationService)
    {
        _integrationService = integrationService;
    }

    [HttpGet("integration/dashboard/{userId}")]
    public async Task<IActionResult> GetIntegrationDashboard(int userId)
    {
        var result = await _integrationService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("integration/forecast/{userId}")]
    public async Task<IActionResult> GetIntegrationForecast(int userId)
    {
        var result = await _integrationService.GetForecastAsync(userId);

        return Ok(result);
    }
}
