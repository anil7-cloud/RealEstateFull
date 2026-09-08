using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-conversion-funnel")]
public class LeadConversionFunnelController : ControllerBase
{
    private readonly LeadConversionFunnelService _service;

    public LeadConversionFunnelController(
        LeadConversionFunnelService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadConversionFunnel funnel)
    {
        return Ok(
            await _service.CreateAsync(funnel));
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


    [HttpGet("current/{leadId}")]
    public async Task<IActionResult> GetCurrentStage(
        int leadId)
    {
        var result = await _service.GetCurrentStageAsync(leadId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpPut]
    public async Task<IActionResult> Update(
        LeadConversionFunnel funnel)
    {
        var result = await _service.UpdateAsync(funnel);

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
