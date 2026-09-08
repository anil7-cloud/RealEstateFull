using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPaymentTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _paymentTrendService;

    public PropertyAiCustomerSalesLeadForecastPaymentTrendController(
        IPropertyAiCustomerSalesLeadForecastService paymentTrendService)
    {
        _paymentTrendService = paymentTrendService;
    }

    [HttpGet("payment-trend/{userId}")]
    public async Task<IActionResult> GetPaymentTrend(int userId)
    {
        var result = await _paymentTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("payment-trend/history/{userId}")]
    public async Task<IActionResult> GetPaymentTrendHistory(int userId)
    {
        var result = await _paymentTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
