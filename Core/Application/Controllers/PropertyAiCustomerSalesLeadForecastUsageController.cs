using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastUsageController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _usageService;

    public PropertyAiCustomerSalesLeadForecastUsageController(
        IPropertyAiCustomerSalesLeadForecastCoreService usageService)
    {
        _usageService = usageService;
    }

    [HttpGet("usage/{userId:int}")]
    public async Task<IActionResult> GetUsage(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result = await _usageService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("usage/dashboard/{userId:int}")]
    public async Task<IActionResult> GetUsageDashboard(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result = await _usageService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("usage/forecast/{userId:int}")]
    public async Task<IActionResult> GetUsageForecast(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result = await _usageService.GetForecastAsync(userId);

        return Ok(result);
    }
}
