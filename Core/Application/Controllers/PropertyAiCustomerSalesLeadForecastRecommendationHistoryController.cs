using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastRecommendationHistoryController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastInsightService _recommendationService;

    public PropertyAiCustomerSalesLeadForecastRecommendationHistoryController(
        IPropertyAiCustomerSalesLeadForecastInsightService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    [HttpGet("recommendation-history/{userId}")]
    public async Task<IActionResult> GetRecommendationHistory(int userId)
    {
        var result = await _recommendationService.GetHistoryAsync(userId);

        return Ok(result);
    }

    [HttpGet("recommendation/{userId}")]
    public async Task<IActionResult> GetRecommendation(int userId)
    {
        var result = await _recommendationService.GetInsightAsync(userId);

        return Ok(result);
    }
}
