using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-workflow-triggers")]
public class LeadWorkflowTriggerController : ControllerBase
{
    private readonly LeadWorkflowTriggerService _service;

    public LeadWorkflowTriggerController(
        LeadWorkflowTriggerService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        int workflowId,
        string triggerType)
    {
        return Ok(
            await _service.CreateAsync(
                workflowId,
                triggerType));
    }


    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        return Ok(
            await _service.GetActiveAsync());
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
