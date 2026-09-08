using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastTrackingController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastActivityService _trackingService;

    public PropertyAiCustomerSalesLeadForecastTrackingController(
        IPropertyAiCustomerSalesLeadForecastActivityService trackingService)
    {
        _trackingService = trackingService;
    }

    [HttpGet("tracking/{userId}")]
    public async Task<IActionResult> GetTracking(int userId)
    {
        var result = await _trackingService.GetActivityAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetTrackingHistory(int userId)
    {
        var result = await _trackingService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
