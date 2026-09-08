using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastDashboardManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastDashboardService _dashboardService;

    public PropertyAiCustomerSalesLeadForecastDashboardManagerController(
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

    [HttpGet("dashboard/history/{userId}")]
    public async Task<IActionResult> GetDashboardHistory(int userId)
    {
        var result = await _dashboardService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
