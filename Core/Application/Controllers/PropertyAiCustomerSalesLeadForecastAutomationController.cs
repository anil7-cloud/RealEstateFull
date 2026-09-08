using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastAutomationController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastAutomationService _automationService;

    public PropertyAiCustomerSalesLeadForecastAutomationController(
        IPropertyAiCustomerSalesLeadForecastAutomationService automationService)
    {
        _automationService = automationService;
    }

    [HttpGet("automation/{userId}")]
    public async Task<IActionResult> GetAutomation(int userId)
    {
        var result = await _automationService.GetAutomationAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _automationService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
