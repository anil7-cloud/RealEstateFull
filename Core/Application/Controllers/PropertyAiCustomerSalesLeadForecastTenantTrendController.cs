using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastTenantTrendController
    : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _tenantTrendService;

    public PropertyAiCustomerSalesLeadForecastTenantTrendController(
        IPropertyAiCustomerSalesLeadForecastService tenantTrendService)
    {
        _tenantTrendService = tenantTrendService;
    }

    [HttpGet("tenant-trend/{userId}")]
    public async Task<IActionResult> GetTenantTrend(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _tenantTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("tenant-trend/history/{userId}")]
    public async Task<IActionResult> GetTenantTrendHistory(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _tenantTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
