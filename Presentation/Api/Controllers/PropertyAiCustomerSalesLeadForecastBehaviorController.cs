using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/ai/forecast-behavior")]
public class PropertyAiCustomerSalesLeadForecastBehaviorController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastBehaviorService _service;


    public PropertyAiCustomerSalesLeadForecastBehaviorController(
        IPropertyAiCustomerSalesLeadForecastBehaviorService service)
    {
        _service = service;
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetBehavior(int userId)
    {
        return Ok(await _service.GetBehaviorAsync(userId));
    }


    [HttpGet("{userId}/history")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        return Ok(await _service.GetHistoryAsync(userId));
    }
}
