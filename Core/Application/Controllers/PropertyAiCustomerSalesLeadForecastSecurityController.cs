using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSecurityController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _securityService;

    public PropertyAiCustomerSalesLeadForecastSecurityController(
        IPropertyAiCustomerSalesLeadForecastCoreService securityService)
    {
        _securityService = securityService;
    }

    [HttpGet("security/status/{userId}")]
    public async Task<IActionResult> GetSecurityStatus(int userId)
    {
        var result = await _securityService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("security/dashboard/{userId}")]
    public async Task<IActionResult> GetSecurityDashboard(int userId)
    {
        var result = await _securityService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
