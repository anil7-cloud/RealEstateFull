using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastBillingTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _billingTrendService;

    public PropertyAiCustomerSalesLeadForecastBillingTrendController(
        IPropertyAiCustomerSalesLeadForecastService billingTrendService)
    {
        _billingTrendService = billingTrendService;
    }

    [HttpGet("billing-trend/{userId}")]
    public async Task<IActionResult> GetBillingTrend(int userId)
    {
        var result = await _billingTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("billing-trend/history/{userId}")]
    public async Task<IActionResult> GetBillingTrendHistory(int userId)
    {
        var result = await _billingTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
