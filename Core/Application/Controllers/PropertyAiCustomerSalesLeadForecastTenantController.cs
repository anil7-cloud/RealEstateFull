using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastTenantController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _tenantService;

    public PropertyAiCustomerSalesLeadForecastTenantController(
        IPropertyAiCustomerSalesLeadForecastCoreService tenantService)
    {
        _tenantService = tenantService;
    }

    [HttpGet("tenant/{userId}")]
    public async Task<IActionResult> GetTenant(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result = await _tenantService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("tenant/forecast/{userId}")]
    public async Task<IActionResult> GetTenantForecast(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result = await _tenantService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("tenant/metrics/{userId}")]
    public async Task<IActionResult> GetTenantMetrics(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result = await _tenantService.GetMetricsAsync(userId);

        return Ok(result);
    }
}
