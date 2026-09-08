using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastQuotaTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _quotaTrendService;

    public PropertyAiCustomerSalesLeadForecastQuotaTrendController(
        IPropertyAiCustomerSalesLeadForecastService quotaTrendService)
    {
        _quotaTrendService = quotaTrendService;
    }

    [HttpGet("quota-trend/{userId}")]
    public async Task<IActionResult> GetQuotaTrend(int userId)
    {
        var result = await _quotaTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("quota-trend/history/{userId}")]
    public async Task<IActionResult> GetQuotaTrendHistory(int userId)
    {
        var result = await _quotaTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
