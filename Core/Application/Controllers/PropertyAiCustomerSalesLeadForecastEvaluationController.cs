using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastEvaluationController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _evaluationService;

    public PropertyAiCustomerSalesLeadForecastEvaluationController(
        IPropertyAiCustomerSalesLeadForecastCoreService evaluationService)
    {
        _evaluationService = evaluationService;
    }

    [HttpGet("evaluation/{userId}")]
    public async Task<IActionResult> GetEvaluation(int userId)
    {
        var result = await _evaluationService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("evaluation/dashboard/{userId}")]
    public async Task<IActionResult> GetEvaluationDashboard(int userId)
    {
        var result = await _evaluationService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
