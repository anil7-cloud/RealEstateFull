using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-workflows")]
public class LeadWorkflowController : ControllerBase
{
    private readonly LeadWorkflowService _service;

    public LeadWorkflowController(
        LeadWorkflowService service)
    {
        _service = service;
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


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadWorkflow workflow)
    {
        return Ok(
            await _service.CreateAsync(workflow));
    }


    [HttpPut]
    public async Task<IActionResult> Update(
        LeadWorkflow workflow)
    {
        var result = await _service.UpdateAsync(workflow);

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
