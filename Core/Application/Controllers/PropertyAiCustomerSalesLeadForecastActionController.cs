using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastActionController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastActionService _actionService;

    public PropertyAiCustomerSalesLeadForecastActionController(
        IPropertyAiCustomerSalesLeadForecastActionService actionService)
    {
        _actionService = actionService;
    }

    [HttpGet("action/{userId}")]
    public async Task<IActionResult> GetAction(int userId)
    {
        var result = await _actionService.GetActionAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _actionService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
