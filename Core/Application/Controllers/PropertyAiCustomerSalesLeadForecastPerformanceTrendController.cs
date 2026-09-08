using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPerformanceTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _performanceTrendService;

    public PropertyAiCustomerSalesLeadForecastPerformanceTrendController(
        IPropertyAiCustomerSalesLeadForecastService performanceTrendService)
    {
        _performanceTrendService = performanceTrendService;
    }

    [HttpGet("performance-trend/{userId}")]
    public async Task<IActionResult> GetPerformanceTrend(int userId)
    {
        var result = await _performanceTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("performance-trend/history/{userId}")]
    public async Task<IActionResult> GetPerformanceTrendHistory(int userId)
    {
        var result = await _performanceTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
