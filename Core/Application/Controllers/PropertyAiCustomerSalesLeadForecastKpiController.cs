using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastKpiController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _kpiService;

    public PropertyAiCustomerSalesLeadForecastKpiController(
        IPropertyAiCustomerSalesLeadForecastCoreService kpiService)
    {
        _kpiService = kpiService;
    }

    [HttpGet("kpi/{userId}")]
    public async Task<IActionResult> GetKpi(int userId)
    {
        var result = await _kpiService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("kpi/dashboard/{userId}")]
    public async Task<IActionResult> GetKpiDashboard(int userId)
    {
        var result = await _kpiService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
