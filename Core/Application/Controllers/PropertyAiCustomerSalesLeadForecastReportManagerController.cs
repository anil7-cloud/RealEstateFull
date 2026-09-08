using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastReportManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastReportService _reportService;

    public PropertyAiCustomerSalesLeadForecastReportManagerController(
        IPropertyAiCustomerSalesLeadForecastReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("report/{userId}")]
    public async Task<IActionResult> GetReport(int userId)
    {
        var result = await _reportService.GetReportAsync(userId);

        return Ok(result);
    }

    [HttpGet("report/history/{userId}")]
    public async Task<IActionResult> GetReportHistory(int userId)
    {
        var result = await _reportService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
