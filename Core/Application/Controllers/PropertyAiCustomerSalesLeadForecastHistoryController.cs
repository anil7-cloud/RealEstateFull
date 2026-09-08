using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastHistoryController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _historyService;

    public PropertyAiCustomerSalesLeadForecastHistoryController(
        IPropertyAiCustomerSalesLeadForecastService historyService)
    {
        _historyService = historyService;
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _historyService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }

    [HttpGet("analysis/{userId}")]
    public async Task<IActionResult> GetAnalysisHistory(int userId)
    {
        var result = await _historyService.AnalyzeAsync(userId);

        return Ok(result);
    }
}
