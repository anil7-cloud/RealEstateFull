using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastKpiTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _kpiTrendService;

    public PropertyAiCustomerSalesLeadForecastKpiTrendController(
        IPropertyAiCustomerSalesLeadForecastService kpiTrendService)
    {
        _kpiTrendService = kpiTrendService;
    }

    [HttpGet("kpi-trend/{userId}")]
    public async Task<IActionResult> GetKpiTrend(int userId)
    {
        var result = await _kpiTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("kpi-trend/history/{userId}")]
    public async Task<IActionResult> GetKpiTrendHistory(int userId)
    {
        var result = await _kpiTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
