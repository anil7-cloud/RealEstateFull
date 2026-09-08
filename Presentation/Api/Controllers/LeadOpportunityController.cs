using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-opportunities")]
public class LeadOpportunityController : ControllerBase
{
    private readonly LeadOpportunityService _service;

    public LeadOpportunityController(
        LeadOpportunityService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadOpportunity opportunity)
    {
        return Ok(
            await _service.CreateAsync(opportunity));
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
        LeadOpportunity opportunity)
    {
        var result = await _service.UpdateAsync(opportunity);

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
