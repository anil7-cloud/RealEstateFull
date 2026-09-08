using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-commissions")]
public class LeadCommissionController : ControllerBase
{
    private readonly LeadCommissionService _service;

    public LeadCommissionController(
        LeadCommissionService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        int leadId,
        int propertyId,
        decimal salePrice,
        decimal commissionRate)
    {
        return Ok(
            await _service.CreateAsync(
                leadId,
                propertyId,
                salePrice,
                commissionRate));
    }


    [HttpGet("lead/{leadId}")]
    public async Task<IActionResult> GetByLead(
        int leadId)
    {
        return Ok(
            await _service.GetByLeadAsync(leadId));
    }


    [HttpPut("{id}/pay")]
    public async Task<IActionResult> Pay(
        int id,
        decimal amount)
    {
        var result = await _service.PayAsync(
            id,
            amount);

        if (!result)
            return NotFound();

        return Ok();
    }
}
