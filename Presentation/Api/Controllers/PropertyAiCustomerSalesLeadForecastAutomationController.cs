using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/ai/forecast-automation")]
public class PropertyAiCustomerSalesLeadForecastAutomationController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastAutomationService _service;


    public PropertyAiCustomerSalesLeadForecastAutomationController(
        IPropertyAiCustomerSalesLeadForecastAutomationService service)
    {
        _service = service;
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAutomation(int userId)
    {
        return Ok(await _service.GetAutomationAsync(userId));
    }


    [HttpGet("{userId}/history")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        return Ok(await _service.GetHistoryAsync(userId));
    }
}
