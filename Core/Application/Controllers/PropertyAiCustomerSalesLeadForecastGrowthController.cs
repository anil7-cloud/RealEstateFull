using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastGrowthController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _growthService;

    public PropertyAiCustomerSalesLeadForecastGrowthController(
        IPropertyAiCustomerSalesLeadForecastService growthService)
    {
        _growthService = growthService;
    }

    [HttpGet("growth/{userId}")]
    public async Task<IActionResult> GetGrowth(int userId)
    {
        var result = await _growthService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("growth/history/{userId}")]
    public async Task<IActionResult> GetGrowthHistory(int userId)
    {
        var result = await _growthService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
