using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSummaryManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _summaryService;

    public PropertyAiCustomerSalesLeadForecastSummaryManagerController(
        IPropertyAiCustomerSalesLeadForecastCoreService summaryService)
    {
        _summaryService = summaryService;
    }

    [HttpGet("summary/{userId}")]
    public async Task<IActionResult> GetSummary(int userId)
    {
        var result = await _summaryService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("summary/forecast/{userId}")]
    public async Task<IActionResult> GetSummaryForecast(int userId)
    {
        var result = await _summaryService.GetForecastAsync(userId);

        return Ok(result);
    }
}
