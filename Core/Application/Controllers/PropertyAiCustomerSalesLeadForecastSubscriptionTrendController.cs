using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSubscriptionTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _subscriptionTrendService;

    public PropertyAiCustomerSalesLeadForecastSubscriptionTrendController(
        IPropertyAiCustomerSalesLeadForecastService subscriptionTrendService)
    {
        _subscriptionTrendService = subscriptionTrendService;
    }

    [HttpGet("subscription-trend/{userId}")]
    public async Task<IActionResult> GetSubscriptionTrend(int userId)
    {
        var result = await _subscriptionTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("subscription-trend/history/{userId}")]
    public async Task<IActionResult> GetSubscriptionTrendHistory(int userId)
    {
        var result = await _subscriptionTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
