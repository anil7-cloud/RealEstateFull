using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastTargetTrendController
    : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _targetTrendService;

    public PropertyAiCustomerSalesLeadForecastTargetTrendController(
        IPropertyAiCustomerSalesLeadForecastService targetTrendService)
    {
        _targetTrendService = targetTrendService;
    }

    [HttpGet("target-trend/{userId}")]
    public async Task<IActionResult> GetTargetTrend(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _targetTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("target-trend/history/{userId}")]
    public async Task<IActionResult> GetTargetTrendHistory(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _targetTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
