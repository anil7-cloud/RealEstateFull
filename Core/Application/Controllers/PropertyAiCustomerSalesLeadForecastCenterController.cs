using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastCenterController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _centerService;

    public PropertyAiCustomerSalesLeadForecastCenterController(
        IPropertyAiCustomerSalesLeadForecastCoreService centerService)
    {
        _centerService = centerService;
    }

    [HttpGet("center/{userId}")]
    public async Task<IActionResult> GetCenter(int userId)
    {
        var result = await _centerService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("center/forecast/{userId}")]
    public async Task<IActionResult> GetCenterForecast(int userId)
    {
        var result = await _centerService.GetForecastAsync(userId);

        return Ok(result);
    }
}
