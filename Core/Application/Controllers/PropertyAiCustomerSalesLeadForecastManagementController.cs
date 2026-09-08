using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastManagementController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _managementService;

    public PropertyAiCustomerSalesLeadForecastManagementController(
        IPropertyAiCustomerSalesLeadForecastCoreService managementService)
    {
        _managementService = managementService;
    }

    [HttpGet("management/dashboard/{userId}")]
    public async Task<IActionResult> GetManagementDashboard(int userId)
    {
        var result = await _managementService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("management/forecast/{userId}")]
    public async Task<IActionResult> GetManagementForecast(int userId)
    {
        var result = await _managementService.GetForecastAsync(userId);

        return Ok(result);
    }
}
