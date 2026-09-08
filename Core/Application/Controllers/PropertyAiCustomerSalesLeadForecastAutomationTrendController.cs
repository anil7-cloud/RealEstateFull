using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastAutomationTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _automationTrendService;

    public PropertyAiCustomerSalesLeadForecastAutomationTrendController(
        IPropertyAiCustomerSalesLeadForecastService automationTrendService)
    {
        _automationTrendService = automationTrendService;
    }

    [HttpGet("automation-trend/{userId}")]
    public async Task<IActionResult> GetAutomationTrend(int userId)
    {
        var result = await _automationTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("automation-trend/history/{userId}")]
    public async Task<IActionResult> GetAutomationTrendHistory(int userId)
    {
        var result = await _automationTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
