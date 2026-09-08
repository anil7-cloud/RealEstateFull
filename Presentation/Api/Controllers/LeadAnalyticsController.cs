using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-analytics")]
public class LeadAnalyticsController : ControllerBase
{
    private readonly LeadAnalyticsService _service;

    public LeadAnalyticsController(LeadAnalyticsService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }


    [HttpGet("lead/{leadId}")]
    public async Task<IActionResult> GetByLead(int leadId)
    {
        return Ok(await _service.GetByLeadAsync(leadId));
    }


    [HttpPost]
    public async Task<IActionResult> Create(LeadAnalytics analytics)
    {
        return Ok(await _service.CreateAsync(analytics));
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok();
    }
}
