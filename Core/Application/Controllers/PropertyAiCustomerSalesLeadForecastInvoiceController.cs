using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastInvoiceController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _invoiceService;

    public PropertyAiCustomerSalesLeadForecastInvoiceController(
        IPropertyAiCustomerSalesLeadForecastCoreService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("invoice/{userId}")]
    public async Task<IActionResult> GetInvoice(int userId)
    {
        var result = await _invoiceService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("invoice/history/{userId}")]
    public async Task<IActionResult> GetInvoiceHistory(int userId)
    {
        var result = await _invoiceService.GetMetricsAsync(userId);

        return Ok(result);
    }
}
