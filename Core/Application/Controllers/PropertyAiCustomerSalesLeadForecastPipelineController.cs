using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPipelineController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _pipelineService;

    public PropertyAiCustomerSalesLeadForecastPipelineController(
        IPropertyAiCustomerSalesLeadForecastService pipelineService)
    {
        _pipelineService = pipelineService;
    }

    [HttpGet("pipeline/{userId}")]
    public async Task<IActionResult> GetPipeline(int userId)
    {
        var result = await _pipelineService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("pipeline/history/{userId}")]
    public async Task<IActionResult> GetPipelineHistory(int userId)
    {
        var result = await _pipelineService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
