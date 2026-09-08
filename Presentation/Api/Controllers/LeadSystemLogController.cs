using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-system-logs")]
public class LeadSystemLogController : ControllerBase
{
    private readonly LeadSystemLogService _service;

    public LeadSystemLogController(
        LeadSystemLogService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadSystemLog log)
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


    [HttpGet("level/{level}")]
    public async Task<IActionResult> GetByLevel(
        string level)
    {
        return Ok(
            await _service.GetByLevelAsync(level));
    }


    [HttpGet("resolved")]
    public async Task<IActionResult> GetResolved()
    {
        return Ok(
            await _service.GetResolvedAsync());
    }


    [HttpGet("unresolved")]
    public async Task<IActionResult> GetUnresolved()
    {
        return Ok(
            await _service.GetUnresolvedAsync());
    }


    [HttpPut("{id}/resolve")]
    public async Task<IActionResult> Resolve(
        int id,
        string notes)
    {
        var result = await _service.ResolveAsync(
            id,
            notes);

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
