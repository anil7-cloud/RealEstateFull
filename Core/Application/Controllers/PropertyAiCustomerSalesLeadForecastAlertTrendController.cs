using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastAlertTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _alertTrendService;

    public PropertyAiCustomerSalesLeadForecastAlertTrendController(
        IPropertyAiCustomerSalesLeadForecastService alertTrendService)
    {
        _alertTrendService = alertTrendService;
    }

    [HttpGet("alert-trend/{userId}")]
    public async Task<IActionResult> GetAlertTrend(int userId)
    {
        var result = await _alertTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("alert-trend/history/{userId}")]
    public async Task<IActionResult> GetAlertTrendHistory(int userId)
    {
        var result = await _alertTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
