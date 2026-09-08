using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/ai/forecast-analysis")]
public class PropertyAiCustomerSalesLeadForecastAnalysisController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastAnalysisService _service;


    public PropertyAiCustomerSalesLeadForecastAnalysisController(
        IPropertyAiCustomerSalesLeadForecastAnalysisService service)
    {
        _service = service;
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> Analyze(int userId)
    {
        return Ok(await _service.AnalyzeAsync(userId));
    }


    [HttpGet("{userId}/history")]
    public async Task<IActionResult> History(int userId)
    {
        return Ok(await _service.GetHistoryAsync(userId));
    }
}
