using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastNotificationController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastNotificationService _notificationService;

    public PropertyAiCustomerSalesLeadForecastNotificationController(
        IPropertyAiCustomerSalesLeadForecastNotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("notification/{userId}")]
    public async Task<IActionResult> GetNotification(int userId)
    {
        var result = await _notificationService.GetNotificationAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _notificationService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
