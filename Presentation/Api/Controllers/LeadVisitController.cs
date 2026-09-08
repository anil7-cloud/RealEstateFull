using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-visits")]
public class LeadVisitController : ControllerBase
{
    private readonly LeadVisitService _service;

    public LeadVisitController(
        LeadVisitService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadVisit visit)
    {
        return Ok(
            await _service.CreateAsync(visit));
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


    [HttpPut]
    public async Task<IActionResult> Update(
        LeadVisit visit)
    {
        var result = await _service.UpdateAsync(visit);

        if (!result)
            return NotFound();

        return Ok();
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
