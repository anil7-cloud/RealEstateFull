using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastOptimizationHistoryController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastOptimizationService _optimizationService;

    public PropertyAiCustomerSalesLeadForecastOptimizationHistoryController(
        IPropertyAiCustomerSalesLeadForecastOptimizationService optimizationService)
    {
        _optimizationService = optimizationService;
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _optimizationService.GetHistoryAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/dashboard/{userId}")]
    public async Task<IActionResult> GetHistoryDashboard(int userId)
    {
        var result = await _optimizationService.GetOptimizationAsync(userId);

        return Ok(result);
    }
}
