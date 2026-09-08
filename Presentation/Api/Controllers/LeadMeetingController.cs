using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-meeting")]
public class LeadMeetingController : ControllerBase
{
    private readonly LeadMeetingService _service;

    public LeadMeetingController(LeadMeetingService service)
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
    public async Task<IActionResult> Create(LeadMeeting meeting)
    {
        return Ok(await _service.CreateAsync(meeting));
    }


    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        string status,
        string result)
    {
        var updated = await _service.UpdateStatusAsync(
            id,
            status);

        if (!updated)
            return NotFound();

        return Ok();
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
