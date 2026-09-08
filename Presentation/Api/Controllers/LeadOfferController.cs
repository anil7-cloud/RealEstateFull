using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-offers")]
public class LeadOfferController : ControllerBase
{
    private readonly LeadOfferService _service;

    public LeadOfferController(
        LeadOfferService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadOffer offer)
    {
        return Ok(
            await _service.CreateAsync(offer));
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


    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        string status)
    {
        var result = await _service.UpdateStatusAsync(
            id,
            status);

        if (!result)
            return NotFound();

        return Ok();
    }


    [HttpPut("{id}/notes")]
    public async Task<IActionResult> UpdateNotes(
        int id,
        string notes)
    {
        var result = await _service.UpdateNotesAsync(
            id,
            notes);

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
