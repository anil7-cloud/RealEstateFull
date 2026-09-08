using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-audit-logs")]
public class LeadAuditLogController : ControllerBase
{
    private readonly LeadAuditLogService _service;

    public LeadAuditLogController(
        LeadAuditLogService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadAuditLog log)
    {
        return Ok(
            await _service.CreateAsync(log));
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await _service.GetAllAsync());
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


    [HttpGet("lead/{leadId}")]
    public async Task<IActionResult> GetByLead(
        int leadId)
    {
        return Ok(
            await _service.GetByLeadAsync(leadId));
    }


    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(
        int userId)
    {
        return Ok(
            await _service.GetByUserAsync(userId));
    }


    [HttpGet("action/{action}")]
    public async Task<IActionResult> GetByAction(
        string action)
    {
        return Ok(
            await _service.GetByActionAsync(action));
    }


    [HttpGet("failed")]
    public async Task<IActionResult> GetFailed()
    {
        return Ok(
            await _service.GetFailedLogsAsync());
    }


    [HttpGet("date-range")]
    public async Task<IActionResult> GetByDateRange(
        DateTime startDate,
        DateTime endDate)
    {
        return Ok(
            await _service.GetByDateRangeAsync(
                startDate,
                endDate));
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
