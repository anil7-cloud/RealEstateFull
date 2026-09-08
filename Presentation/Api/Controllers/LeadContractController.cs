using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-contracts")]
public class LeadContractController : ControllerBase
{
    private readonly LeadContractService _service;


    public LeadContractController(
        LeadContractService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        int leadId,
        int propertyId,
        string contractType,
        decimal amount)
    {
        return Ok(
            await _service.CreateAsync(
                leadId,
                propertyId,
                contractType,
                amount));
    }


    [HttpGet("lead/{leadId}")]
    public async Task<IActionResult> GetByLead(
        int leadId)
    {
        return Ok(
            await _service.GetByLeadAsync(leadId));
    }


    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        string status)
    {
        var result = await _service.UpdateStatusAsync(
            id,
            status);

        if (!result)
            return NotFound();

        return Ok();
    }
}
