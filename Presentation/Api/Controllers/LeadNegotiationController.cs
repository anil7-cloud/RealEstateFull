using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-negotiations")]
public class LeadNegotiationController : ControllerBase
{
    private readonly LeadNegotiationService _service;

    public LeadNegotiationController(
        LeadNegotiationService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadNegotiation negotiation)
    {
        return Ok(
            await _service.CreateAsync(negotiation));
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
        LeadNegotiation negotiation)
    {
        var result = await _service.UpdateAsync(negotiation);

        if (!result)
            return NotFound();

        return Ok();
    }


    [HttpPut("{id}/close")]
    public async Task<IActionResult> Close(
        int id,
        string status)
    {
        var result = await _service.CloseAsync(
            id,
            status);

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
