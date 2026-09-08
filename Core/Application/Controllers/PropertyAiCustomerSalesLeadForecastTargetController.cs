using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastTargetController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _targetService;

    public PropertyAiCustomerSalesLeadForecastTargetController(
        IPropertyAiCustomerSalesLeadForecastCoreService targetService)
    {
        _targetService = targetService;
    }

    [HttpGet("target/{userId}")]
    public async Task<IActionResult> GetTarget(int userId)
    {
        var result = await _targetService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("target/dashboard/{userId}")]
    public async Task<IActionResult> GetTargetDashboard(int userId)
    {
        var result = await _targetService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
