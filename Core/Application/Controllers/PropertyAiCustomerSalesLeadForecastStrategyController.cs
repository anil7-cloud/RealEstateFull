using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastStrategyController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastStrategyService _strategyService;

    public PropertyAiCustomerSalesLeadForecastStrategyController(
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

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _strategyService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
