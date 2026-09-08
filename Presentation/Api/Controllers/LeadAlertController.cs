using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-alerts")]
public class LeadAlertController : ControllerBase
{
    private readonly LeadAlertService _service;

    public LeadAlertController(
        LeadAlertService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        int leadId,
        string alertName,
        string city,
        string propertyType)
    {
        return Ok(
            await _service.CreateAsync(
                leadId,
                alertName,
                city,
                propertyType));
    }


    [HttpGet("lead/{leadId}")]
    public async Task<IActionResult> GetByLead(
        int leadId)
    {
        return Ok(
            await _service.GetByLeadAsync(leadId));
    }


    [HttpPut("{id}/disable")]
    public async Task<IActionResult> Disable(
        int id)
    {
        var result = await _service.DisableAsync(id);

        if (!result)
            return NotFound();

        return Ok();
    }
}
