using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPortalController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _portalService;

    public PropertyAiCustomerSalesLeadForecastPortalController(
        IPropertyAiCustomerSalesLeadForecastCoreService portalService)
    {
        _portalService = portalService;
    }

    [HttpGet("portal/dashboard/{userId}")]
    public async Task<IActionResult> GetPortalDashboard(int userId)
    {
        var result = await _portalService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("portal/forecast/{userId}")]
    public async Task<IActionResult> GetPortalForecast(int userId)
    {
        var result = await _portalService.GetForecastAsync(userId);

        return Ok(result);
    }
}
