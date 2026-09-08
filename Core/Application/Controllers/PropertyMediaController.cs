using Core.Application.DTOs.PropertyMedia;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyMediaController : ControllerBase
{
    private readonly IPropertyMediaService _propertyMediaService;

    public PropertyMediaController(
        IPropertyMediaService propertyMediaService)
    {
        _propertyMediaService = propertyMediaService;
    }

    [HttpGet("property/{propertyId:int}")]
    public async Task<IActionResult> GetByPropertyId(
        int propertyId,
        CancellationToken cancellationToken)
    {
        var result = await _propertyMediaService.GetByPropertyIdAsync(
            propertyId,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _propertyMediaService.GetByIdAsync(
            id,
            cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePropertyMediaDto dto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = await _propertyMediaService.CreateAsync(
            dto,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdatePropertyMediaDto dto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var updated = await _propertyMediaService.UpdateAsync(
            id,
            dto,
            cancellationToken);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await _propertyMediaService.DeleteAsync(
            id,
            cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpPut("{id:int}/primary")]
    public async Task<IActionResult> SetPrimary(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _propertyMediaService.SetPrimaryAsync(
            id,
            cancellationToken);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpPut("property/{propertyId:int}/reorder")]
    public async Task<IActionResult> Reorder(
        int propertyId,
        [FromBody] List<int> mediaIds,
        CancellationToken cancellationToken)
    {
        if (mediaIds == null || mediaIds.Count == 0)
            return BadRequest("mediaIds boş olamaz.");

        var result = await _propertyMediaService.ReorderAsync(
            propertyId,
            mediaIds,
            cancellationToken);

        if (!result)
            return NotFound();

        return NoContent();
    }
}
