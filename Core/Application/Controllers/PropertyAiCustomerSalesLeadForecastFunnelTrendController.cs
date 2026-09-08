using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastFunnelTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _funnelTrendService;

    public PropertyAiCustomerSalesLeadForecastFunnelTrendController(
        IPropertyAiCustomerSalesLeadForecastService funnelTrendService)
    {
        _funnelTrendService = funnelTrendService;
    }

    [HttpGet("funnel-trend/{userId}")]
    public async Task<IActionResult> GetFunnelTrend(int userId)
    {
        var result = await _funnelTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("funnel-trend/history/{userId}")]
    public async Task<IActionResult> GetFunnelTrendHistory(int userId)
    {
        var result = await _funnelTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
