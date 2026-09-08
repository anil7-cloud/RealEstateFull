using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastMarketTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _marketTrendService;

    public PropertyAiCustomerSalesLeadForecastMarketTrendController(
        IPropertyAiCustomerSalesLeadForecastService marketTrendService)
    {
        _marketTrendService = marketTrendService;
    }

    [HttpGet("market-trend/{userId}")]
    public async Task<IActionResult> GetMarketTrend(int userId)
    {
        var result = await _marketTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("market-trend/history/{userId}")]
    public async Task<IActionResult> GetMarketTrendHistory(int userId)
    {
        var result = await _marketTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
