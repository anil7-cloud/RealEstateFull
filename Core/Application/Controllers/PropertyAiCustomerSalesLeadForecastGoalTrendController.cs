using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastGoalTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _goalTrendService;

    public PropertyAiCustomerSalesLeadForecastGoalTrendController(
        IPropertyAiCustomerSalesLeadForecastService goalTrendService)
    {
        _goalTrendService = goalTrendService;
    }

    [HttpGet("goal-trend/{userId}")]
    public async Task<IActionResult> GetGoalTrend(int userId)
    {
        var result = await _goalTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("goal-trend/history/{userId}")]
    public async Task<IActionResult> GetGoalTrendHistory(int userId)
    {
        var result = await _goalTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
