using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Http;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace Services;

public class PropertyMediaUploadService
{
    private readonly AppDbContext _context;
    private readonly REAL_ESTATE_CLEAN.Infrastructure.Storage.ImageStorageService _storage;

    private static readonly string[] AllowedImageExtensions =
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    };

    private static readonly string[] AllowedVideoExtensions =
    {
        ".mp4",
        ".mov",
        ".webm"
    };

    public PropertyMediaUploadService(
        AppDbContext context,
        REAL_ESTATE_CLEAN.Infrastructure.Storage.ImageStorageService storage)
    {
        _context = context;
        _storage = storage;
    }

    public async Task<PropertyMedia> UploadAsync(
        int propertyId,
        IFormFile file,
        string mediaType = "Image",
        CancellationToken cancellationToken = default)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Dosya boş olamaz.");

        if (propertyId <= 0)
            throw new ArgumentException("Geçerli bir PropertyId gerekli.");

        var extension = Path.GetExtension(file.FileName)
            .ToLowerInvariant();

        var allowedExtensions =
            mediaType.Equals("Video", StringComparison.OrdinalIgnoreCase)
                ? AllowedVideoExtensions
                : AllowedImageExtensions;

        if (!allowedExtensions.Contains(extension))
            throw new ArgumentException(
                $"Desteklenmeyen dosya türü: {extension}");

        var safeFileName =
            $"{Guid.NewGuid():N}{extension}";

        var mediaUrl = _storage.Upload(safeFileName);

        var existingCount = _context.PropertyMedia
            .Count(
                x => x.PropertyId == propertyId && x.IsActive);

        var entity = new PropertyMedia
        {
            PropertyId = propertyId,
            MediaUrl = mediaUrl,
            MediaType = mediaType,
            DisplayOrder = existingCount,
            IsPrimary = existingCount == 0,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.PropertyMedia.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
