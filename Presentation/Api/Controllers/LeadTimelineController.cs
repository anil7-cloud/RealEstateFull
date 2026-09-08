using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-timelines")]
public class LeadTimelineController : ControllerBase
{
    private readonly LeadTimelineService _service;


    public LeadTimelineController(
        LeadTimelineService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadTimeline timeline)
    {
        return Ok(
            await _service.CreateAsync(timeline));
    }


    [HttpGet("lead/{leadId}")]
    public async Task<IActionResult> GetByLead(
        int leadId)
    {
        return Ok(
            await _service.GetByLeadAsync(leadId));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        string title,
        string description)
    {
        var result = await _service.UpdateAsync(
            id,
            title,
            description);

        if (!result)
            return NotFound();

        return Ok();
    }


    [HttpPut("{id}/hide")]
    public async Task<IActionResult> Hide(
        int id)
    {
        var result = await _service.HideAsync(id);

        if (!result)
            return NotFound();

        return Ok();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok();
    }
}
