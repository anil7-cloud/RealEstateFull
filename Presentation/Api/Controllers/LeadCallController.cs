using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-call")]
public class LeadCallController : ControllerBase
{
    private readonly LeadCallService _service;


    public LeadCallController(LeadCallService service)
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
    public async Task<IActionResult> Create(LeadCall call)
    {
        return Ok(await _service.CreateAsync(call));
    }


    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        string status,
        string result)
    {
        var updated = await _service.UpdateStatusAsync(
            id,
            status,
            result);


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
