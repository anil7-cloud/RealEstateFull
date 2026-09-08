using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-activity-logs")]
public class LeadActivityLogController : ControllerBase
{
    private readonly LeadActivityLogService _service;

    public LeadActivityLogController(
        LeadActivityLogService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        int leadId,
        string action,
        string entityName,
        string notes)
    {
        return Ok(
            await _service.CreateAsync(
                leadId,
                action,
                entityName,
                notes));
    }


    [HttpGet("lead/{leadId}")]
    public async Task<IActionResult> GetByLead(
        int leadId)
    {
        return Ok(
            await _service.GetByLeadAsync(leadId));
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
