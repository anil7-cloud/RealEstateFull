using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastInvoiceTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _invoiceTrendService;

    public PropertyAiCustomerSalesLeadForecastInvoiceTrendController(
        IPropertyAiCustomerSalesLeadForecastService invoiceTrendService)
    {
        _invoiceTrendService = invoiceTrendService;
    }

    [HttpGet("invoice-trend/{userId}")]
    public async Task<IActionResult> GetInvoiceTrend(int userId)
    {
        var result = await _invoiceTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("invoice-trend/history/{userId}")]
    public async Task<IActionResult> GetInvoiceTrendHistory(int userId)
    {
        var result = await _invoiceTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
