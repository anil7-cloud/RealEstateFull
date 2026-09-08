using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-report-templates")]
public class LeadReportTemplateController : ControllerBase
{
    private readonly LeadReportTemplateService _service;

    public LeadReportTemplateController(
        LeadReportTemplateService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadReportTemplate template)
    {
        return Ok(await _service.CreateAsync(template));
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


    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetByCode(
        string code)
    {
        var result = await _service.GetByCodeAsync(code);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetByCategory(
        string category)
    {
        return Ok(await _service.GetByCategoryAsync(category));
    }


    [HttpPut("{id}/used")]
    public async Task<IActionResult> MarkAsUsed(int id)
    {
        var result = await _service.MarkAsUsedAsync(id);

        if (!result)
            return NotFound();

        return Ok();
    }


    [HttpPut]
    public async Task<IActionResult> Update(
        LeadReportTemplate template)
    {
        var result = await _service.UpdateAsync(template);

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
