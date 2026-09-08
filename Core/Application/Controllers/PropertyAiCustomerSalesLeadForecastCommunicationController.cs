using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastCommunicationController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCommunicationService _communicationService;

    public PropertyAiCustomerSalesLeadForecastCommunicationController(
        IPropertyAiCustomerSalesLeadForecastCommunicationService communicationService)
    {
        _communicationService = communicationService;
    }

    [HttpGet("communication/{userId}")]
    public async Task<IActionResult> GetCommunication(int userId)
    {
        var result = await _communicationService.GetCommunicationAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _communicationService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
