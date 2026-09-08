using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastMessageManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastMessageService _messageService;

    public PropertyAiCustomerSalesLeadForecastMessageManagerController(
        IPropertyAiCustomerSalesLeadForecastMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet("message/{userId}")]
    public async Task<IActionResult> GetMessage(int userId)
    {
        var result = await _messageService.GetMessageAsync(userId);

        return Ok(result);
    }

    [HttpGet("message/history/{userId}")]
    public async Task<IActionResult> GetMessageHistory(int userId)
    {
        var result = await _messageService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
