using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-workflow-steps")]
public class LeadWorkflowStepController : ControllerBase
{
    private readonly LeadWorkflowStepService _service;

    public LeadWorkflowStepController(
        LeadWorkflowStepService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        int workflowId,
        string stepName,
        int order)
    {
        return Ok(
            await _service.CreateAsync(
                workflowId,
                stepName,
                order));
    }


    [HttpGet("workflow/{workflowId}")]
    public async Task<IActionResult> GetByWorkflow(
        int workflowId)
    {
        return Ok(
            await _service.GetByWorkflowAsync(workflowId));
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


    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(
        int id)
    {
        var result = await _service.CompleteAsync(id);

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
