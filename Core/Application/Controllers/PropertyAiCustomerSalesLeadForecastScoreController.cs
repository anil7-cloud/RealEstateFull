using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastScoreController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _scoreService;

    public PropertyAiCustomerSalesLeadForecastScoreController(
        IPropertyAiCustomerSalesLeadForecastCoreService scoreService)
    {
        _scoreService = scoreService;
    }

    [HttpGet("score/{userId}")]
    public async Task<IActionResult> GetScore(int userId)
    {
        var result = await _scoreService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("score/dashboard/{userId}")]
    public async Task<IActionResult> GetScoreDashboard(int userId)
    {
        var result = await _scoreService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
