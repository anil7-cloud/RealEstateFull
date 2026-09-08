using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastLearningController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _learningService;

    public PropertyAiCustomerSalesLeadForecastLearningController(
        IPropertyAiCustomerSalesLeadForecastCoreService learningService)
    {
        _learningService = learningService;
    }

    [HttpGet("learning/{userId}")]
    public async Task<IActionResult> GetLearning(int userId)
    {
        var result = await _learningService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("learning/dashboard/{userId}")]
    public async Task<IActionResult> GetLearningDashboard(int userId)
    {
        var result = await _learningService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
