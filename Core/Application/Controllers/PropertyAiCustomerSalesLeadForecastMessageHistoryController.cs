using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastMessageHistoryController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastMessageHistoryService _historyService;

    public PropertyAiCustomerSalesLeadForecastMessageHistoryController(
        IPropertyAiCustomerSalesLeadForecastMessageHistoryService historyService)
    {
        _historyService = historyService;
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _historyService.GetHistoryAsync(userId);

        return Ok(result);
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save(
        PropertyAiCustomerSalesLeadForecastMessageHistoryDto dto)
    {
        var result = await _historyService.SaveAsync(dto);

        return Ok(result);
    }
}
