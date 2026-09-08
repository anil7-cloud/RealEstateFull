using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/ai/forecast-activity")]
public class PropertyAiCustomerSalesLeadForecastActivityController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastActivityService _service;


    public PropertyAiCustomerSalesLeadForecastActivityController(
        IPropertyAiCustomerSalesLeadForecastActivityService service)
    {
        _service = service;
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetActivity(int userId)
    {
        return Ok(await _service.GetActivityAsync(userId));
    }


    [HttpGet("{userId}/history")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        return Ok(await _service.GetHistoryAsync(userId));
    }
}
