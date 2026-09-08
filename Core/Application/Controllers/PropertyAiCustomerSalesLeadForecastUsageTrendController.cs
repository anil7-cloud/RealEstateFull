using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastUsageTrendController
    : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _usageTrendService;

    public PropertyAiCustomerSalesLeadForecastUsageTrendController(
        IPropertyAiCustomerSalesLeadForecastService usageTrendService)
    {
        _usageTrendService = usageTrendService;
    }

    [HttpGet("usage-trend/{userId:int}")]
    public async Task<IActionResult> GetUsageTrend(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _usageTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("usage-trend/history/{userId:int}")]
    public async Task<IActionResult> GetUsageTrendHistory(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _usageTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
