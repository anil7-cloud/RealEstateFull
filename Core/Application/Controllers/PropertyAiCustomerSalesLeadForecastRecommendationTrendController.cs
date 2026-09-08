using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastRecommendationTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _recommendationTrendService;

    public PropertyAiCustomerSalesLeadForecastRecommendationTrendController(
        IPropertyAiCustomerSalesLeadForecastService recommendationTrendService)
    {
        _recommendationTrendService = recommendationTrendService;
    }

    [HttpGet("recommendation-trend/{userId}")]
    public async Task<IActionResult> GetRecommendationTrend(int userId)
    {
        var result = await _recommendationTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("recommendation-trend/history/{userId}")]
    public async Task<IActionResult> GetRecommendationTrendHistory(int userId)
    {
        var result = await _recommendationTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
