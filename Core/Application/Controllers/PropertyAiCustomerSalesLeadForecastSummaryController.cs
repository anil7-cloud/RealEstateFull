using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSummaryController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastSummaryService _summaryService;

    public PropertyAiCustomerSalesLeadForecastSummaryController(
        IPropertyAiCustomerSalesLeadForecastSummaryService summaryService)
    {
        _summaryService = summaryService;
    }

    [HttpGet("summary/{userId}")]
    public async Task<IActionResult> GetSummary(int userId)
    {
        var result = await _summaryService.GetSummaryAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _summaryService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
