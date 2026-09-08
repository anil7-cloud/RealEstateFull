using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-activities")]
public class LeadActivityController : ControllerBase
{
    private readonly LeadActivityService _service;

    public LeadActivityController(
        LeadActivityService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }


    [HttpGet("lead/{leadId}")]
    public async Task<IActionResult> GetByLead(
        int leadId)
    {
        return Ok(
            await _service.GetByLeadAsync(leadId));
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadActivity activity)
    {
        return Ok(
            await _service.CreateAsync(activity));
    }


    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(
        int id,
        string result)
    {
        var response = await _service.CompleteAsync(
            id,
            result);

        if (!response)
            return NotFound();

        return Ok();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        var response = await _service.DeleteAsync(id);

        if (!response)
            return NotFound();

        return Ok();
    }
}
