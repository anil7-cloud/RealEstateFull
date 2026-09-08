using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastNotificationCenterController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastNotificationService _notificationService;

    public PropertyAiCustomerSalesLeadForecastNotificationCenterController(
        IPropertyAiCustomerSalesLeadForecastNotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("center/{userId}")]
    public async Task<IActionResult> GetCenter(int userId)
    {
        var result = await _notificationService.GetNotificationAsync(userId);

        return Ok(result);
    }

    [HttpGet("center/history/{userId}")]
    public async Task<IActionResult> GetCenterHistory(int userId)
    {
        var result = await _notificationService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
