using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastNotificationTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _notificationTrendService;

    public PropertyAiCustomerSalesLeadForecastNotificationTrendController(
        IPropertyAiCustomerSalesLeadForecastService notificationTrendService)
    {
        _notificationTrendService = notificationTrendService;
    }

    [HttpGet("notification-trend/{userId}")]
    public async Task<IActionResult> GetNotificationTrend(int userId)
    {
        var result = await _notificationTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("notification-trend/history/{userId}")]
    public async Task<IActionResult> GetNotificationTrendHistory(int userId)
    {
        var result = await _notificationTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
