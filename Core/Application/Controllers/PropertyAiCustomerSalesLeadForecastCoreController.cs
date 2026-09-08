using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastCoreController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _coreService;

    public PropertyAiCustomerSalesLeadForecastCoreController(
        IPropertyAiCustomerSalesLeadForecastCoreService coreService)
    {
        _coreService = coreService;
    }


    [HttpGet("forecast/{userId}")]
    public async Task<IActionResult> GetForecast(int userId)
    {
        var result = await _coreService.GetForecastAsync(userId);

        return Ok(result);
    }


    [HttpGet("analysis/{userId}")]
    public async Task<IActionResult> GetAnalysis(int userId)
    {
        var result = await _coreService.GetAnalysisAsync(userId);

        return Ok(result);
    }


    [HttpGet("dashboard/{userId}")]
    public async Task<IActionResult> GetDashboard(int userId)
    {
        var result = await _coreService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
