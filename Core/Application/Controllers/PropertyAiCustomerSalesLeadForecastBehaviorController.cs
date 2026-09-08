using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastBehaviorController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastBehaviorService _behaviorService;

    public PropertyAiCustomerSalesLeadForecastBehaviorController(
        IPropertyAiCustomerSalesLeadForecastBehaviorService behaviorService)
    {
        _behaviorService = behaviorService;
    }

    [HttpGet("behavior/{userId}")]
    public async Task<IActionResult> GetBehavior(int userId)
    {
        var result = await _behaviorService.GetBehaviorAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _behaviorService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
