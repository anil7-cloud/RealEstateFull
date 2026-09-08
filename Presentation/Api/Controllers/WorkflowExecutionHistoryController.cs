using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/workflow-execution-history")]
public class WorkflowExecutionHistoryController : ControllerBase
{
    private readonly WorkflowExecutionHistoryService _service;

    public WorkflowExecutionHistoryController(
        WorkflowExecutionHistoryService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        Guid workflowId,
        Guid userId,
        string action,
        string? notes = null)
    {
        return Ok(
            await _service.CreateAsync(
                workflowId,
                userId,
                action,
                notes));
    }


    [HttpGet("workflow/{workflowId}")]
    public async Task<IActionResult> GetByWorkflow(
        Guid workflowId)
    {
        return Ok(
            await _service.GetByWorkflowAsync(workflowId));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        string status)
    {
        var result = await _service.UpdateStatusAsync(
            id,
            status);

        if (!result)
            return NotFound();

        return Ok();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        Guid id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok();
    }
}
