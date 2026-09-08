using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-reports")]
public class LeadReportController : ControllerBase
{
    private readonly LeadReportService _service;

    public LeadReportController(LeadReportService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(LeadReport report)
    {
        return Ok(await _service.CreateAsync(report));
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetByCategory(string category)
    {
        return Ok(await _service.GetByCategoryAsync(category));
    }


    [HttpGet("scheduled")]
    public async Task<IActionResult> GetScheduled()
    {
        return Ok(await _service.GetScheduledAsync());
    }


    [HttpPut("{id}/generated")]
    public async Task<IActionResult> MarkGenerated(int id)
    {
        var result = await _service.MarkGeneratedAsync(id);

        if (!result)
            return NotFound();

        return Ok();
    }


    [HttpPut]
    public async Task<IActionResult> Update(LeadReport report)
    {
        var result = await _service.UpdateAsync(report);

        if (!result)
            return NotFound();

        return Ok();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok();
    }
}
