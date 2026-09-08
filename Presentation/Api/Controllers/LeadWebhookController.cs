using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-webhooks")]
public class LeadWebhookController : ControllerBase
{
    private readonly LeadWebhookService _service;

    public LeadWebhookController(
        LeadWebhookService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadWebhook webhook)
    {
        return Ok(
            await _service.CreateAsync(webhook));
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


    [HttpGet("enabled")]
    public async Task<IActionResult> GetEnabled()
    {
        return Ok(
            await _service.GetEnabledAsync());
    }


    [HttpGet("event/{eventName}")]
    public async Task<IActionResult> GetByEvent(
        string eventName)
    {
        return Ok(
            await _service.GetByEventAsync(eventName));
    }


    [HttpPut]
    public async Task<IActionResult> Update(
        LeadWebhook webhook)
    {
        var result = await _service.UpdateAsync(webhook);

        if (!result)
            return NotFound();

        return Ok();
    }


    [HttpPut("{id}/execute")]
    public async Task<IActionResult> MarkExecuted(
        int id,
        bool success,
        int? statusCode,
        string response)
    {
        var result = await _service.MarkExecutedAsync(
            id,
            success,
            statusCode,
            response);

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
