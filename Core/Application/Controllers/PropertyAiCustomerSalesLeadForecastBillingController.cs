using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastBillingController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _billingService;

    public PropertyAiCustomerSalesLeadForecastBillingController(
        IPropertyAiCustomerSalesLeadForecastCoreService billingService)
    {
        _billingService = billingService;
    }

    [HttpGet("billing/{userId}")]
    public async Task<IActionResult> GetBilling(int userId)
    {
        var result = await _billingService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("billing/status/{userId}")]
    public async Task<IActionResult> GetBillingStatus(int userId)
    {
        var result = await _billingService.GetMetricsAsync(userId);

        return Ok(result);
    }
}
