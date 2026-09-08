using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastBehaviorTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _behaviorTrendService;

    public PropertyAiCustomerSalesLeadForecastBehaviorTrendController(
        IPropertyAiCustomerSalesLeadForecastService behaviorTrendService)
    {
        _behaviorTrendService = behaviorTrendService;
    }

    [HttpGet("behavior-trend/{userId}")]
    public async Task<IActionResult> GetBehaviorTrend(int userId)
    {
        var result = await _behaviorTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("behavior-trend/history/{userId}")]
    public async Task<IActionResult> GetBehaviorTrendHistory(int userId)
    {
        var result = await _behaviorTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
