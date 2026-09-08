using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastExportHistoryController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastReportService _exportService;

    public PropertyAiCustomerSalesLeadForecastExportHistoryController(
        IPropertyAiCustomerSalesLeadForecastReportService exportService)
    {
        _exportService = exportService;
    }

    [HttpGet("export-history/{userId}")]
    public async Task<IActionResult> GetExportHistory(int userId)
    {
        var result = await _exportService.GetHistoryAsync(userId);

        return Ok(result);
    }

    [HttpGet("export/{userId}")]
    public async Task<IActionResult> GetExport(int userId)
    {
        var result = await _exportService.GetReportAsync(userId);

        return Ok(result);
    }
}
