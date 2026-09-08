using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-win-reasons")]
public class LeadWinReasonController : ControllerBase
{
    private readonly LeadWinReasonService _service;

    public LeadWinReasonController(
        LeadWinReasonService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadWinReason winReason)
    {
        return Ok(
            await _service.CreateAsync(winReason));
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


    [HttpGet("top-sales")]
    public async Task<IActionResult> GetTopSales()
    {
        return Ok(
            await _service.GetTopSalesAsync());
    }


    [HttpPut]
    public async Task<IActionResult> Update(
        LeadWinReason winReason)
    {
        var result = await _service.UpdateAsync(winReason);

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
