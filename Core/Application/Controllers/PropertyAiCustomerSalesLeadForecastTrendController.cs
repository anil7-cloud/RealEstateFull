using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _trendService;

    public PropertyAiCustomerSalesLeadForecastTrendController(
        IPropertyAiCustomerSalesLeadForecastService trendService)
    {
        _trendService = trendService;
    }

    [HttpGet("trend/{userId}")]
    public async Task<IActionResult> GetTrend(int userId)
    {
        var result = await _trendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("trend/history/{userId}")]
    public async Task<IActionResult> GetTrendHistory(int userId)
    {
        var result = await _trendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
