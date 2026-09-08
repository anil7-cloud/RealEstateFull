using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastControlController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _controlService;

    public PropertyAiCustomerSalesLeadForecastControlController(
        IPropertyAiCustomerSalesLeadForecastCoreService controlService)
    {
        _controlService = controlService;
    }

    [HttpGet("control/{userId}")]
    public async Task<IActionResult> GetControl(int userId)
    {
        var result = await _controlService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("control/dashboard/{userId}")]
    public async Task<IActionResult> GetControlDashboard(int userId)
    {
        var result = await _controlService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
