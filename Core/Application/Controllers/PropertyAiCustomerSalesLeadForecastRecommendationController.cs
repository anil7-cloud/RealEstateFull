using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastRecommendationController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastInsightService _recommendationService;

    public PropertyAiCustomerSalesLeadForecastRecommendationController(
        IPropertyAiCustomerSalesLeadForecastInsightService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    [HttpGet("recommendation/{userId}")]
    public async Task<IActionResult> GetRecommendation(int userId)
    {
        var result = await _recommendationService.GetInsightAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _recommendationService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
