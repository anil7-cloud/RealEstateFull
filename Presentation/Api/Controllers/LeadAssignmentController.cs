using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-assignments")]
public class LeadAssignmentController : ControllerBase
{
    private readonly LeadAssignmentService _service;

    public LeadAssignmentController(
        LeadAssignmentService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        int leadId,
        int assignedUserId,
        string reason)
    {
        return Ok(
            await _service.CreateAsync(
                leadId,
                assignedUserId,
                reason));
    }


    [HttpGet("lead/{leadId}")]
    public async Task<IActionResult> GetByLead(
        int leadId)
    {
        return Ok(
            await _service.GetByLeadAsync(leadId));
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
}
