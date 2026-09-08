using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-reminders")]
public class LeadReminderController : ControllerBase
{
    private readonly LeadReminderService _service;

    public LeadReminderController(
        LeadReminderService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadReminder reminder)
    {
        return Ok(
            await _service.CreateAsync(reminder));
    }


    [HttpGet("lead/{leadId}")]
    public async Task<IActionResult> GetByLead(
        int leadId)
    {
        return Ok(
            await _service.GetByLeadAsync(leadId));
    }


    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcoming()
    {
        return Ok(
            await _service.GetUpcomingAsync());
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


    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(
        int id)
    {
        var result = await _service.CompleteAsync(id);

        if (!result)
            return NotFound();

        return Ok();
    }


    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> Cancel(
        int id)
    {
        var result = await _service.CancelAsync(id);

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
