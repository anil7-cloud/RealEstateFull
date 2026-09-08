using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastInsightManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastInsightService _insightService;

    public PropertyAiCustomerSalesLeadForecastInsightManagerController(
        IPropertyAiCustomerSalesLeadForecastInsightService insightService)
    {
        _insightService = insightService;
    }

    [HttpGet("insight/{userId}")]
    public async Task<IActionResult> GetInsight(int userId)
    {
        var result = await _insightService.GetInsightAsync(userId);

        return Ok(result);
    }

    [HttpGet("insight/history/{userId}")]
    public async Task<IActionResult> GetInsightHistory(int userId)
    {
        var result = await _insightService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
