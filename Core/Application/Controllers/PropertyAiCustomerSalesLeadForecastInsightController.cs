using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastInsightController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastInsightService _insightService;

    public PropertyAiCustomerSalesLeadForecastInsightController(
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

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _insightService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
