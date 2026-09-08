using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastActivityTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _activityTrendService;

    public PropertyAiCustomerSalesLeadForecastActivityTrendController(
        IPropertyAiCustomerSalesLeadForecastService activityTrendService)
    {
        _activityTrendService = activityTrendService;
    }

    [HttpGet("activity-trend/{userId}")]
    public async Task<IActionResult> GetActivityTrend(int userId)
    {
        var result = await _activityTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("activity-trend/history/{userId}")]
    public async Task<IActionResult> GetActivityTrendHistory(int userId)
    {
        var result = await _activityTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
