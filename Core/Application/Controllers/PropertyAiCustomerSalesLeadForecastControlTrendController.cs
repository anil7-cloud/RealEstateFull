using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastControlTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _controlTrendService;

    public PropertyAiCustomerSalesLeadForecastControlTrendController(
        IPropertyAiCustomerSalesLeadForecastService controlTrendService)
    {
        _controlTrendService = controlTrendService;
    }

    [HttpGet("control-trend/{userId}")]
    public async Task<IActionResult> GetControlTrend(int userId)
    {
        var result = await _controlTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("control-trend/history/{userId}")]
    public async Task<IActionResult> GetControlTrendHistory(int userId)
    {
        var result = await _controlTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
