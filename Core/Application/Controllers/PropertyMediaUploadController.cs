using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyMediaUploadController : ControllerBase
{
    private readonly PropertyMediaUploadService _uploadService;

    public PropertyMediaUploadController(
        PropertyMediaUploadService uploadService)
    {
        _uploadService = uploadService;
    }

    [HttpPost("{propertyId:int}")]
    [RequestSizeLimit(100_000_000)]
    public async Task<IActionResult> Upload(
        int propertyId,
        IFormFile file,
        [FromForm] string mediaType = "Image",
        CancellationToken cancellationToken = default)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Dosya gönderilmedi.");
        }

        if (propertyId <= 0)
        {
            return BadRequest("Geçersiz PropertyId.");
        }

        try
        {
            var result = await _uploadService.UploadAsync(
                propertyId,
                file,
                mediaType,
                cancellationToken);

            return Ok(new
            {
                Message = "Medya başarıyla yüklendi.",
                result.Id,
                result.PropertyId,
                result.MediaUrl,
                result.MediaType,
                result.DisplayOrder,
                result.IsPrimary,
                result.CreatedAt
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                Message = ex.Message
            });
        }
    }
}
