using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeadController : ControllerBase
{
    private readonly LeadService _leadService;

    public LeadController(LeadService leadService)
    {
        _leadService = leadService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var leads = await _leadService.GetAllAsync();

        return Ok(leads);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var lead = await _leadService.GetByIdAsync(id);

        if (lead == null)
            return NotFound();

        return Ok(lead);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Lead request)
    {
        var result = await _leadService.CreateAsync(request);

        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _leadService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok(new
        {
            message = "Lead deleted"
        });
    }
}
