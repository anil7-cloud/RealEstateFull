using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastApiController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _apiService;

    public PropertyAiCustomerSalesLeadForecastApiController(
        IPropertyAiCustomerSalesLeadForecastCoreService apiService)
    {
        _apiService = apiService;
    }

    [HttpGet("forecast/{userId}")]
    public async Task<IActionResult> GetForecast(int userId)
    {
        var result = await _apiService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("dashboard/{userId}")]
    public async Task<IActionResult> GetDashboard(int userId)
    {
        var result = await _apiService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("metrics/{userId}")]
    public async Task<IActionResult> GetMetrics(int userId)
    {
        var result = await _apiService.GetMetricsAsync(userId);

        return Ok(result);
    }
}
