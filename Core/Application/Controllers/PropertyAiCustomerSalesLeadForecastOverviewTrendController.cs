using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastOverviewTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _overviewTrendService;

    public PropertyAiCustomerSalesLeadForecastOverviewTrendController(
        IPropertyAiCustomerSalesLeadForecastService overviewTrendService)
    {
        _overviewTrendService = overviewTrendService;
    }

    [HttpGet("overview-trend/{userId}")]
    public async Task<IActionResult> GetOverviewTrend(int userId)
    {
        var result = await _overviewTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("overview-trend/history/{userId}")]
    public async Task<IActionResult> GetOverviewTrendHistory(int userId)
    {
        var result = await _overviewTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
