using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastAdminController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _adminService;

    public PropertyAiCustomerSalesLeadForecastAdminController(
        IPropertyAiCustomerSalesLeadForecastCoreService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("admin/dashboard/{userId}")]
    public async Task<IActionResult> GetAdminDashboard(int userId)
    {
        var result = await _adminService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("admin/metrics/{userId}")]
    public async Task<IActionResult> GetAdminMetrics(int userId)
    {
        var result = await _adminService.GetMetricsAsync(userId);

        return Ok(result);
    }
}
