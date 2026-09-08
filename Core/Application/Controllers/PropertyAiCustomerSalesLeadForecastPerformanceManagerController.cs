using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPerformanceManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastPerformanceService _performanceService;

    public PropertyAiCustomerSalesLeadForecastPerformanceManagerController(
        IPropertyAiCustomerSalesLeadForecastPerformanceService performanceService)
    {
        _performanceService = performanceService;
    }

    [HttpGet("performance/{userId}")]
    public async Task<IActionResult> GetPerformance(int userId)
    {
        var result = await _performanceService.GetPerformanceAsync(userId);

        return Ok(result);
    }

    [HttpGet("performance/history/{userId}")]
    public async Task<IActionResult> GetPerformanceHistory(int userId)
    {
        var result = await _performanceService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
