using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-tag-groups")]
public class LeadTagGroupController : ControllerBase
{
    private readonly LeadTagGroupService _service;

    public LeadTagGroupController(
        LeadTagGroupService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadTagGroup tagGroup)
    {
        return Ok(
            await _service.CreateAsync(tagGroup));
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


    [HttpPut]
    public async Task<IActionResult> Update(
        LeadTagGroup tagGroup)
    {
        var result = await _service.UpdateAsync(tagGroup);

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
