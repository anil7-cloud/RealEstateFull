using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-workflow-run")]
public class LeadWorkflowRunController : ControllerBase
{
    private readonly LeadWorkflowRunService _service;

    public LeadWorkflowRunController(
        LeadWorkflowRunService service)
    {
        _service = service;
    }


    [HttpPost("{workflowId}/lead/{leadId}")]
    public async Task<IActionResult> Run(
        int workflowId,
        int leadId)
    {
        await _service.RunAsync(
            workflowId,
            leadId);

        return Ok(new
        {
            message = "Workflow çalıştırıldı"
        });
    }
}
