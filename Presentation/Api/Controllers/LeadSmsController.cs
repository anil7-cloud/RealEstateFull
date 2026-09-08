using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-sms")]
public class LeadSmsController : ControllerBase
{
    private readonly LeadSmsService _service;

    public LeadSmsController(LeadSmsService service)
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
    public async Task<IActionResult> Create(LeadSms sms)
    {
        return Ok(await _service.CreateAsync(sms));
    }


    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, string status)
    {
        var result = await _service.UpdateStatusAsync(id, status);

        if (!result)
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
