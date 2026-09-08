using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-workflow-executions")]
public class LeadWorkflowExecutionController : ControllerBase
{
    private readonly LeadWorkflowExecutionService _service;


    public LeadWorkflowExecutionController(
        LeadWorkflowExecutionService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        int leadId,
        int workflowId)
    {
        return Ok(
            await _service.CreateAsync(
                leadId,
                workflowId));
    }


    [HttpGet("lead/{leadId}")]
    public async Task<IActionResult> GetByLead(
        int leadId)
    {
        return Ok(
            await _service.GetByLeadAsync(leadId));
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


    [HttpPut("{id}/fail")]
    public async Task<IActionResult> Fail(
        int id,
        string error)
    {
        var result = await _service.FailAsync(
            id,
            error);

        if (!result)
            return NotFound();

        return Ok();
    }
}
