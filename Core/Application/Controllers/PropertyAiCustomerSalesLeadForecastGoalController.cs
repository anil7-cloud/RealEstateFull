using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastGoalController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _goalService;

    public PropertyAiCustomerSalesLeadForecastGoalController(
        IPropertyAiCustomerSalesLeadForecastCoreService goalService)
    {
        _goalService = goalService;
    }

    [HttpGet("goal/{userId}")]
    public async Task<IActionResult> GetGoal(int userId)
    {
        var result = await _goalService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("goal/dashboard/{userId}")]
    public async Task<IActionResult> GetGoalDashboard(int userId)
    {
        var result = await _goalService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
