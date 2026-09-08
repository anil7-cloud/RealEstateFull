using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastOptimizationTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _optimizationTrendService;

    public PropertyAiCustomerSalesLeadForecastOptimizationTrendController(
        IPropertyAiCustomerSalesLeadForecastService optimizationTrendService)
    {
        _optimizationTrendService = optimizationTrendService;
    }

    [HttpGet("optimization-trend/{userId}")]
    public async Task<IActionResult> GetOptimizationTrend(int userId)
    {
        var result = await _optimizationTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("optimization-trend/history/{userId}")]
    public async Task<IActionResult> GetOptimizationTrendHistory(int userId)
    {
        var result = await _optimizationTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
