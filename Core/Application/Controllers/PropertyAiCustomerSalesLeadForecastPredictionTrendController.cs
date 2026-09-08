using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPredictionTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _predictionTrendService;

    public PropertyAiCustomerSalesLeadForecastPredictionTrendController(
        IPropertyAiCustomerSalesLeadForecastService predictionTrendService)
    {
        _predictionTrendService = predictionTrendService;
    }

    [HttpGet("prediction-trend/{userId}")]
    public async Task<IActionResult> GetPredictionTrend(int userId)
    {
        var result = await _predictionTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("prediction-trend/history/{userId}")]
    public async Task<IActionResult> GetPredictionTrendHistory(int userId)
    {
        var result = await _predictionTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
