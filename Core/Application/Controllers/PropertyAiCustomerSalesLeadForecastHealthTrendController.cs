using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastHealthTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _healthTrendService;

    public PropertyAiCustomerSalesLeadForecastHealthTrendController(
        IPropertyAiCustomerSalesLeadForecastService healthTrendService)
    {
        _healthTrendService = healthTrendService;
    }

    [HttpGet("health-trend/{userId}")]
    public async Task<IActionResult> GetHealthTrend(int userId)
    {
        var result = await _healthTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("health-trend/history/{userId}")]
    public async Task<IActionResult> GetHealthTrendHistory(int userId)
    {
        var result = await _healthTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
