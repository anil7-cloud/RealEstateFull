using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastLeadController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _leadService;

    public PropertyAiCustomerSalesLeadForecastLeadController(
        IPropertyAiCustomerSalesLeadForecastService leadService)
    {
        _leadService = leadService;
    }

    [HttpGet("lead/{userId}")]
    public async Task<IActionResult> GetLeadForecast(int userId)
    {
        var result = await _leadService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("lead-history/{userId}")]
    public async Task<IActionResult> GetLeadHistory(int userId)
    {
        var result = await _leadService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
