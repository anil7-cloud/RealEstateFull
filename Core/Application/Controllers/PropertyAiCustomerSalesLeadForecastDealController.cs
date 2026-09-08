using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastDealController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _dealService;

    public PropertyAiCustomerSalesLeadForecastDealController(
        IPropertyAiCustomerSalesLeadForecastService dealService)
    {
        _dealService = dealService;
    }

    [HttpGet("deal/{userId}")]
    public async Task<IActionResult> GetDeal(int userId)
    {
        var result = await _dealService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("deal/history/{userId}")]
    public async Task<IActionResult> GetDealHistory(int userId)
    {
        var result = await _dealService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
