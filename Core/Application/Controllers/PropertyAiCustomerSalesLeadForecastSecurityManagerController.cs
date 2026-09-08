using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSecurityManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _securityService;

    public PropertyAiCustomerSalesLeadForecastSecurityManagerController(
        IPropertyAiCustomerSalesLeadForecastCoreService securityService)
    {
        _securityService = securityService;
    }

    [HttpGet("security/{userId}")]
    public async Task<IActionResult> GetSecurity(int userId)
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
