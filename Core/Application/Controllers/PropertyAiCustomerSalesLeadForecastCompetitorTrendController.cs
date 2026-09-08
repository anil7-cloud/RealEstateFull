using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastCompetitorTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _competitorTrendService;

    public PropertyAiCustomerSalesLeadForecastCompetitorTrendController(
        IPropertyAiCustomerSalesLeadForecastService competitorTrendService)
    {
        _competitorTrendService = competitorTrendService;
    }

    [HttpGet("competitor-trend/{userId}")]
    public async Task<IActionResult> GetCompetitorTrend(int userId)
    {
        var result = await _competitorTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("competitor-trend/history/{userId}")]
    public async Task<IActionResult> GetCompetitorTrendHistory(int userId)
    {
        var result = await _competitorTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
