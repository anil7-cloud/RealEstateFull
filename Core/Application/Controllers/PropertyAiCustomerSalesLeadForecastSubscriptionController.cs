using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSubscriptionController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _subscriptionService;

    public PropertyAiCustomerSalesLeadForecastSubscriptionController(
        IPropertyAiCustomerSalesLeadForecastCoreService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet("subscription/{userId}")]
    public async Task<IActionResult> GetSubscription(int userId)
    {
        var result = await _subscriptionService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("subscription/status/{userId}")]
    public async Task<IActionResult> GetSubscriptionStatus(int userId)
    {
        var result = await _subscriptionService.GetForecastAsync(userId);

        return Ok(result);
    }
}
