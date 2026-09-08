using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPredictionController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _predictionService;

    public PropertyAiCustomerSalesLeadForecastPredictionController(
        IPropertyAiCustomerSalesLeadForecastService predictionService)
    {
        _predictionService = predictionService;
    }

    [HttpGet("prediction/{userId}")]
    public async Task<IActionResult> GetPrediction(int userId)
    {
        var result = await _predictionService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("analysis/{userId}")]
    public async Task<IActionResult> GetPredictionAnalysis(int userId)
    {
        var result = await _predictionService.AnalyzeAsync(userId);

        return Ok(result);
    }
}
