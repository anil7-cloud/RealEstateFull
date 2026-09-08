using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/ai/forecast-action")]
public class PropertyAiCustomerSalesLeadForecastActionController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastActionService _service;


    public PropertyAiCustomerSalesLeadForecastActionController(
        IPropertyAiCustomerSalesLeadForecastActionService service)
    {
        _service = service;
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAction(int userId)
    {
        return Ok(await _service.GetActionAsync(userId));
    }


    [HttpGet("{userId}/history")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        return Ok(await _service.GetHistoryAsync(userId));
    }
}
