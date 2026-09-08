using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/properties")]
public class PropertyMediaController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _environment;

    public PropertyMediaController(
        AppDbContext db,
        IWebHostEnvironment environment)
    {
        _db = db;
        _environment = environment;
    }

    [HttpGet("{propertyId:int}/media")]
    public async Task<IActionResult> GetMedia(
        int propertyId,
        CancellationToken cancellationToken)
    {
        var media = await _db.PropertyMedia
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.IsCover)
            .ThenBy(x => x.DisplayOrder)
            .ThenBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return Ok(media);
    }

    [HttpPost("{propertyId:int}/media/images")]
    [RequestSizeLimit(104857600)]
    public async Task<IActionResult> UploadImages(
        int propertyId,
        [FromForm] List<IFormFile> files,
        CancellationToken cancellationToken)
    {
        var propertyExists = await _db.Properties
            .AnyAsync(
                x => x.Id == propertyId,
                cancellationToken);

        if (!propertyExists)
        {
            return NotFound(new
            {
                message = "İlan bulunamadı."
            });
        }

        if (files == null || files.Count == 0)
        {
            return BadRequest(new
            {
                message = "En az bir fotoğraf seçmelisiniz."
            });
        }

        var allowedExtensions = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase)
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };

        var uploadDirectory = Path.Combine(
            _environment.WebRootPath,
            "uploads",
            "properties",
            "images",
            propertyId.ToString());

        Directory.CreateDirectory(uploadDirectory);

        var currentCount = await _db.PropertyMedia
            .CountAsync(
                x =>
                    x.PropertyId == propertyId &&
                    x.MediaType == "Image",
                cancellationToken);

        var hasCover = await _db.PropertyMedia
            .AnyAsync(
                x =>
                    x.PropertyId == propertyId &&
                    x.MediaType == "Image" &&
                    x.IsCover,
                cancellationToken);

        var uploaded = new List<PropertyMedia>();

        foreach (var file in files)
        {
            if (file.Length == 0)
                continue;

            if (file.Length > 20 * 1024 * 1024)
            {
                return BadRequest(new
                {
                    message =
                        $"{file.FileName}: Fotoğraf en fazla 20 MB olabilir."
                });
            }

            var extension =
                Path.GetExtension(file.FileName);

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest(new
                {
                    message =
                        $"{file.FileName}: Desteklenmeyen fotoğraf türü."
                });
            }

            var storedFileName =
                $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";

            var physicalPath =
                Path.Combine(
                    uploadDirectory,
                    storedFileName);

            await using (var stream =
                System.IO.File.Create(physicalPath))
            {
                await file.CopyToAsync(
                    stream,
                    cancellationToken);
            }

            currentCount++;

            var url =
                $"/uploads/properties/images/{propertyId}/{storedFileName}";

            var item = new PropertyMedia
            {
                PropertyId = propertyId,
                MediaType = "Image",
                Url = url,
                ThumbnailUrl = url,
                OriginalFileName = file.FileName,
                ContentType = file.ContentType,
                SizeBytes = file.Length,
                DisplayOrder = currentCount,
                IsCover = !hasCover && uploaded.Count == 0,
                CreatedAt = DateTime.UtcNow
            };

            _db.PropertyMedia.Add(item);
            uploaded.Add(item);
        }

        await _db.SaveChangesAsync(
            cancellationToken);

        return Ok(uploaded);
    }

    [HttpPost("{propertyId:int}/media/video")]
    [RequestSizeLimit(524288000)]
    public async Task<IActionResult> UploadVideo(
        int propertyId,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var propertyExists = await _db.Properties
            .AnyAsync(
                x => x.Id == propertyId,
                cancellationToken);

        if (!propertyExists)
        {
            return NotFound(new
            {
                message = "İlan bulunamadı."
            });
        }

        if (file == null || file.Length == 0)
        {
            return BadRequest(new
            {
                message = "Video seçmelisiniz."
            });
        }

        if (file.Length > 500 * 1024 * 1024)
        {
            return BadRequest(new
            {
                message = "Video en fazla 500 MB olabilir."
            });
        }

        var extension =
            Path.GetExtension(file.FileName);

        var allowedExtensions = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase)
        {
            ".mp4",
            ".webm",
            ".mov"
        };

        if (!allowedExtensions.Contains(extension))
        {
            return BadRequest(new
            {
                message =
                    "Desteklenen video türleri: MP4, WEBM ve MOV."
            });
        }

        var uploadDirectory = Path.Combine(
            _environment.WebRootPath,
            "uploads",
            "properties",
            "videos",
            propertyId.ToString());

        Directory.CreateDirectory(uploadDirectory);

        var storedFileName =
            $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";

        var physicalPath =
            Path.Combine(
                uploadDirectory,
                storedFileName);

        await using (var stream =
            System.IO.File.Create(physicalPath))
        {
            await file.CopyToAsync(
                stream,
                cancellationToken);
        }

        var videoCount = await _db.PropertyMedia
            .CountAsync(
                x =>
                    x.PropertyId == propertyId &&
                    x.MediaType == "Video",
                cancellationToken);

        var url =
            $"/uploads/properties/videos/{propertyId}/{storedFileName}";

        var media = new PropertyMedia
        {
            PropertyId = propertyId,
            MediaType = "Video",
            Url = url,
            OriginalFileName = file.FileName,
            ContentType = file.ContentType,
            SizeBytes = file.Length,
            DisplayOrder = videoCount + 1,
            IsCover = false,
            CreatedAt = DateTime.UtcNow
        };

        _db.PropertyMedia.Add(media);

        await _db.SaveChangesAsync(
            cancellationToken);

        return Ok(media);
    }

    [HttpDelete("media/{mediaId:int}")]
    public async Task<IActionResult> DeleteMedia(
        int mediaId,
        CancellationToken cancellationToken)
    {
        var media = await _db.PropertyMedia
            .FirstOrDefaultAsync(
                x => x.Id == mediaId,
                cancellationToken);

        if (media == null)
        {
            return NotFound(new
            {
                message = "Medya bulunamadı."
            });
        }

        var relativePath =
            media.Url.TrimStart('/')
                .Replace('/', Path.DirectorySeparatorChar);

        var physicalPath =
            Path.Combine(
                _environment.WebRootPath,
                relativePath);

        if (System.IO.File.Exists(physicalPath))
        {
            System.IO.File.Delete(physicalPath);
        }

        var wasCover = media.IsCover;
        var propertyId = media.PropertyId;

        _db.PropertyMedia.Remove(media);

        await _db.SaveChangesAsync(
            cancellationToken);

        if (wasCover)
        {
            var nextImage = await _db.PropertyMedia
                .Where(x =>
                    x.PropertyId == propertyId &&
                    x.MediaType == "Image")
                .OrderBy(x => x.DisplayOrder)
                .FirstOrDefaultAsync(cancellationToken);

            if (nextImage != null)
            {
                nextImage.IsCover = true;

                await _db.SaveChangesAsync(
                    cancellationToken);
            }
        }

        return NoContent();
    }

    [HttpPut("media/{mediaId:int}/cover")]
    public async Task<IActionResult> SetCover(
        int mediaId,
        CancellationToken cancellationToken)
    {
        var media = await _db.PropertyMedia
            .FirstOrDefaultAsync(
                x => x.Id == mediaId,
                cancellationToken);

        if (media == null)
        {
            return NotFound(new
            {
                message = "Fotoğraf bulunamadı."
            });
        }

        if (media.MediaType != "Image")
        {
            return BadRequest(new
            {
                message =
                    "Yalnızca fotoğraf kapak olarak ayarlanabilir."
            });
        }

        var images = await _db.PropertyMedia
            .Where(x =>
                x.PropertyId == media.PropertyId &&
                x.MediaType == "Image")
            .ToListAsync(cancellationToken);

        foreach (var image in images)
        {
            image.IsCover =
                image.Id == media.Id;
        }

        await _db.SaveChangesAsync(
            cancellationToken);

        return Ok(media);
    }

    [HttpPut("{propertyId:int}/media/reorder")]
    public async Task<IActionResult> ReorderMedia(
        int propertyId,
        [FromBody] List<MediaOrderRequest> request,
        CancellationToken cancellationToken)
    {
        if (request == null || request.Count == 0)
        {
            return BadRequest(new
            {
                message = "Medya sıralama listesi boş olamaz."
            });
        }

        var ids = request
            .Select(x => x.MediaId)
            .Distinct()
            .ToList();

        var mediaItems = await _db.PropertyMedia
            .Where(x =>
                x.PropertyId == propertyId &&
                ids.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (mediaItems.Count != ids.Count)
        {
            return BadRequest(new
            {
                message =
                    "Gönderilen medya kayıtlarından bazıları bu ilana ait değil."
            });
        }

        foreach (var item in mediaItems)
        {
            var requestedOrder =
                request.First(x =>
                    x.MediaId == item.Id);

            item.DisplayOrder =
                requestedOrder.DisplayOrder;
        }

        await _db.SaveChangesAsync(
            cancellationToken);

        var result = await _db.PropertyMedia
            .Where(x =>
                x.PropertyId == propertyId)
            .OrderBy(x => x.MediaType)
            .ThenBy(x => x.DisplayOrder)
            .ToListAsync(cancellationToken);

        return Ok(result);
    }

}


public class MediaOrderRequest
{
    public int MediaId { get; set; }

    public int DisplayOrder { get; set; }
}
