using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastRevenueController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _revenueService;

    public PropertyAiCustomerSalesLeadForecastRevenueController(
        IPropertyAiCustomerSalesLeadForecastService revenueService)
    {
        _revenueService = revenueService;
    }

    [HttpGet("revenue/{userId}")]
    public async Task<IActionResult> GetRevenue(int userId)
    {
        var result = await _revenueService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("revenue/history/{userId}")]
    public async Task<IActionResult> GetRevenueHistory(int userId)
    {
        var result = await _revenueService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
