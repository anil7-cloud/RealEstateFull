using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastAlertController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastNotificationService _alertService;

    public PropertyAiCustomerSalesLeadForecastAlertController(
        IPropertyAiCustomerSalesLeadForecastNotificationService alertService)
    {
        _alertService = alertService;
    }

    [HttpGet("alert/{userId}")]
    public async Task<IActionResult> GetAlert(int userId)
    {
        var result = await _alertService.GetNotificationAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetAlertHistory(int userId)
    {
        var result = await _alertService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
