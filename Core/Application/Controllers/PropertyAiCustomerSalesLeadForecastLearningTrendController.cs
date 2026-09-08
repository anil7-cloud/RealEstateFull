using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastLearningTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _learningTrendService;

    public PropertyAiCustomerSalesLeadForecastLearningTrendController(
        IPropertyAiCustomerSalesLeadForecastService learningTrendService)
    {
        _learningTrendService = learningTrendService;
    }

    [HttpGet("learning-trend/{userId}")]
    public async Task<IActionResult> GetLearningTrend(int userId)
    {
        var result = await _learningTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("learning-trend/history/{userId}")]
    public async Task<IActionResult> GetLearningTrendHistory(int userId)
    {
        var result = await _learningTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
