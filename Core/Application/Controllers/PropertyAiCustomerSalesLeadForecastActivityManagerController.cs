using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastActivityManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastActivityService _activityService;

    public PropertyAiCustomerSalesLeadForecastActivityManagerController(
        IPropertyAiCustomerSalesLeadForecastActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet("activity/{userId}")]
    public async Task<IActionResult> GetActivity(int userId)
    {
        var result = await _activityService.GetActivityAsync(userId);

        return Ok(result);
    }

    [HttpGet("activity/history/{userId}")]
    public async Task<IActionResult> GetActivityHistory(int userId)
    {
        var result = await _activityService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
