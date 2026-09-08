using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-source-details")]
public class LeadSourceDetailController : ControllerBase
{
    private readonly LeadSourceDetailService _service;

    public LeadSourceDetailController(
        LeadSourceDetailService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadSourceDetail sourceDetail)
    {
        return Ok(
            await _service.CreateAsync(sourceDetail));
    }


    [HttpGet("lead/{leadId}")]
    public async Task<IActionResult> GetByLead(
        int leadId)
    {
        return Ok(
            await _service.GetByLeadAsync(leadId));
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


    [HttpGet("converted")]
    public async Task<IActionResult> GetConverted()
    {
        return Ok(
            await _service.GetConvertedAsync());
    }


    [HttpPut]
    public async Task<IActionResult> Update(
        LeadSourceDetail sourceDetail)
    {
        var result = await _service.UpdateAsync(sourceDetail);

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
