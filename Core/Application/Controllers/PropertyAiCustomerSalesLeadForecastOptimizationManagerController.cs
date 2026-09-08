using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastOptimizationManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastOptimizationService _optimizationService;

    public PropertyAiCustomerSalesLeadForecastOptimizationManagerController(
        IPropertyAiCustomerSalesLeadForecastOptimizationService optimizationService)
    {
        _optimizationService = optimizationService;
    }

    [HttpGet("optimization/{userId}")]
    public async Task<IActionResult> GetOptimization(int userId)
    {
        var result = await _optimizationService.GetOptimizationAsync(userId);

        return Ok(result);
    }

    [HttpGet("optimization/history/{userId}")]
    public async Task<IActionResult> GetOptimizationHistory(int userId)
    {
        var result = await _optimizationService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
