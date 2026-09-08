using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastDashboardController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastDashboardService _dashboardService;

    public PropertyAiCustomerSalesLeadForecastDashboardController(
        IPropertyAiCustomerSalesLeadForecastDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("dashboard/{userId}")]
    public async Task<IActionResult> GetDashboard(int userId)
    {
        var result = await _dashboardService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _dashboardService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
