using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastRevenueTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _revenueTrendService;

    public PropertyAiCustomerSalesLeadForecastRevenueTrendController(
        IPropertyAiCustomerSalesLeadForecastService revenueTrendService)
    {
        _revenueTrendService = revenueTrendService;
    }

    [HttpGet("revenue-trend/{userId}")]
    public async Task<IActionResult> GetRevenueTrend(int userId)
    {
        var result = await _revenueTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("revenue-trend/history/{userId}")]
    public async Task<IActionResult> GetRevenueTrendHistory(int userId)
    {
        var result = await _revenueTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
