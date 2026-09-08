using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-dashboard")]
public class LeadDashboardController : ControllerBase
{
    private readonly LeadDashboardService _service;

    public LeadDashboardController(
        LeadDashboardService service)
    {
        _service = service;
    }


    [HttpGet("summary")]
    public async Task<IActionResult> Summary()
    {
        return Ok(await _service.GetSummaryAsync());
    }


    [HttpGet("statistics")]
    public async Task<IActionResult> Statistics()
    {
        return Ok(await _service.GetStatisticsAsync());
    }


    [HttpGet("pipeline")]
    public async Task<IActionResult> Pipeline()
    {
        return Ok(await _service.GetPipelineAsync());
    }


    [HttpGet("performance")]
    public async Task<IActionResult> Performance()
    {
        return Ok(await _service.GetPerformanceAsync());
    }
}
