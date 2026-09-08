using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastEvaluationTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _evaluationTrendService;

    public PropertyAiCustomerSalesLeadForecastEvaluationTrendController(
        IPropertyAiCustomerSalesLeadForecastService evaluationTrendService)
    {
        _evaluationTrendService = evaluationTrendService;
    }

    [HttpGet("evaluation-trend/{userId}")]
    public async Task<IActionResult> GetEvaluationTrend(int userId)
    {
        var result = await _evaluationTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("evaluation-trend/history/{userId}")]
    public async Task<IActionResult> GetEvaluationTrendHistory(int userId)
    {
        var result = await _evaluationTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
