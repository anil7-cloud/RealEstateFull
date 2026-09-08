using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastConversionController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastConversionService _conversionService;

    public PropertyAiCustomerSalesLeadForecastConversionController(
        IPropertyAiCustomerSalesLeadForecastConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    [HttpGet("conversion/{userId}")]
    public async Task<IActionResult> GetConversion(int userId)
    {
        var result = await _conversionService.GetConversionAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _conversionService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
