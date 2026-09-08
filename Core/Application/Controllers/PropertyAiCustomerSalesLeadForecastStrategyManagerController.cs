using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastStrategyManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastStrategyService _strategyService;

    public PropertyAiCustomerSalesLeadForecastStrategyManagerController(
        IPropertyAiCustomerSalesLeadForecastStrategyService strategyService)
    {
        _strategyService = strategyService;
    }

    [HttpGet("strategy/{userId}")]
    public async Task<IActionResult> GetStrategy(int userId)
    {
        var result = await _strategyService.GetStrategyAsync(userId);

        return Ok(result);
    }

    [HttpGet("strategy/history/{userId}")]
    public async Task<IActionResult> GetStrategyHistory(int userId)
    {
        var result = await _strategyService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
