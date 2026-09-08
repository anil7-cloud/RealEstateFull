using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPaymentController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _paymentService;

    public PropertyAiCustomerSalesLeadForecastPaymentController(
        IPropertyAiCustomerSalesLeadForecastCoreService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("payment/{userId}")]
    public async Task<IActionResult> GetPayment(int userId)
    {
        var result = await _paymentService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("payment/history/{userId}")]
    public async Task<IActionResult> GetPaymentHistory(int userId)
    {
        var result = await _paymentService.GetMetricsAsync(userId);

        return Ok(result);
    }
}
