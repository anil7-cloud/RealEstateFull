using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastMessageController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastMessageService _messageService;

    public PropertyAiCustomerSalesLeadForecastMessageController(
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

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _messageService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
