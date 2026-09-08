using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/lead-import-jobs")]
public class LeadImportJobController : ControllerBase
{
    private readonly LeadImportJobService _service;

    public LeadImportJobController(
        LeadImportJobService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        LeadImportJob job)
    {
        return Ok(await _service.CreateAsync(job));
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


    [HttpGet("pending")]
    public async Task<IActionResult> GetPending()
    {
        return Ok(await _service.GetPendingAsync());
    }


    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        return Ok(await _service.GetByUserAsync(userId));
    }


    [HttpPut("{id}/progress")]
    public async Task<IActionResult> UpdateProgress(
        int id,
        int processedRows,
        int successRows,
        int failedRows)
    {
        var result = await _service.UpdateProgressAsync(
            id,
            processedRows,
            successRows,
            failedRows);

        if (!result)
            return NotFound();

        return Ok();
    }


    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(
        int id,
        string fileName,
        string filePath,
        long fileSize)
    {
        var result = await _service.CompleteAsync(
            id,
            fileName,
            filePath,
            fileSize);

        if (!result)
            return NotFound();

        return Ok();
    }


    [HttpPut("{id}/fail")]
    public async Task<IActionResult> Fail(
        int id,
        string error)
    {
        var result = await _service.FailAsync(id, error);

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
