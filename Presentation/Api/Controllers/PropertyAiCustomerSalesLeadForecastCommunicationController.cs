using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/ai/forecast-communication")]
public class PropertyAiCustomerSalesLeadForecastCommunicationController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCommunicationService _service;


    public PropertyAiCustomerSalesLeadForecastCommunicationController(
        IPropertyAiCustomerSalesLeadForecastCommunicationService service)
    {
        _service = service;
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCommunication(int userId)
    {
        return Ok(await _service.GetCommunicationAsync(userId));
    }


    [HttpGet("{userId}/history")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        return Ok(await _service.GetHistoryAsync(userId));
    }
}
