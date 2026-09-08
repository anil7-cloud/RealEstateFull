using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastConversionTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _conversionTrendService;

    public PropertyAiCustomerSalesLeadForecastConversionTrendController(
        IPropertyAiCustomerSalesLeadForecastService conversionTrendService)
    {
        _conversionTrendService = conversionTrendService;
    }

    [HttpGet("conversion-trend/{userId}")]
    public async Task<IActionResult> GetConversionTrend(int userId)
    {
        var result = await _conversionTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("conversion-trend/history/{userId}")]
    public async Task<IActionResult> GetConversionTrendHistory(int userId)
    {
        var result = await _conversionTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
