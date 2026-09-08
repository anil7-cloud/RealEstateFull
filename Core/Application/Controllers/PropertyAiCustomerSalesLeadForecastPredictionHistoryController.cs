using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPredictionHistoryController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _predictionService;

    public PropertyAiCustomerSalesLeadForecastPredictionHistoryController(
        IPropertyAiCustomerSalesLeadForecastService predictionService)
    {
        _predictionService = predictionService;
    }

    [HttpGet("prediction-history/{userId}")]
    public async Task<IActionResult> GetPredictionHistory(int userId)
    {
        var result = await _predictionService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }

    [HttpGet("prediction-analysis/{userId}")]
    public async Task<IActionResult> GetPredictionAnalysis(int userId)
    {
        var result = await _predictionService.AnalyzeAsync(userId);

        return Ok(result);
    }
}
