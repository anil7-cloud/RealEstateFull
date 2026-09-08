using Core.Application.DTOs.PropertyMedia;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Services;
public class PropertyMediaService : IPropertyMediaService
{
    private readonly DbContext _context;

    public PropertyMediaService(DbContext context)
    {
        _context = context;
    }

    public async Task<List<PropertyMediaDto>> GetByPropertyIdAsync(
        int propertyId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<PropertyMedia>()
            .AsNoTracking()
            .Where(x => x.PropertyId == propertyId && x.IsActive)
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.Id)
            .Select(x => new PropertyMediaDto
            {
                Id = x.Id,
                PropertyId = x.PropertyId,
                MediaUrl = x.MediaUrl,
                ThumbnailUrl = x.ThumbnailUrl,
                MediaType = x.MediaType,
                Title = x.Title,
                Description = x.Description,
                DisplayOrder = x.DisplayOrder,
                IsPrimary = x.IsPrimary,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PropertyMediaDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<PropertyMedia>()
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new PropertyMediaDto
            {
                Id = x.Id,
                PropertyId = x.PropertyId,
                MediaUrl = x.MediaUrl,
                ThumbnailUrl = x.ThumbnailUrl,
                MediaType = x.MediaType,
                Title = x.Title,
                Description = x.Description,
                DisplayOrder = x.DisplayOrder,
                IsPrimary = x.IsPrimary,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PropertyMediaDto> CreateAsync(
        CreatePropertyMediaDto dto,
        CancellationToken cancellationToken = default)
    {
        if (dto.IsPrimary)
        {
            var existingPrimary = await _context.Set<PropertyMedia>()
                .Where(x =>
                    x.PropertyId == dto.PropertyId &&
                    x.IsPrimary &&
                    x.IsActive)
                .ToListAsync(cancellationToken);

            foreach (var media in existingPrimary)
            {
                media.IsPrimary = false;
                media.UpdatedAt = DateTime.UtcNow;
            }
        }

        var entity = new PropertyMedia
        {
            PropertyId = dto.PropertyId,
            MediaUrl = dto.MediaUrl,
            ThumbnailUrl = dto.ThumbnailUrl,
            MediaType = dto.MediaType,
            Title = dto.Title,
            Description = dto.Description,
            DisplayOrder = dto.DisplayOrder,
            IsPrimary = dto.IsPrimary,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Set<PropertyMedia>().Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new PropertyMediaDto
        {
            Id = entity.Id,
            PropertyId = entity.PropertyId,
            MediaUrl = entity.MediaUrl,
            ThumbnailUrl = entity.ThumbnailUrl,
            MediaType = entity.MediaType,
            Title = entity.Title,
            Description = entity.Description,
            DisplayOrder = entity.DisplayOrder,
            IsPrimary = entity.IsPrimary,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdatePropertyMediaDto dto,
        CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<PropertyMedia>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return false;

        if (dto.IsPrimary)
        {
            var existingPrimary = await _context.Set<PropertyMedia>()
                .Where(x =>
                    x.PropertyId == entity.PropertyId &&
                    x.Id != id &&
                    x.IsPrimary &&
                    x.IsActive)
                .ToListAsync(cancellationToken);

            foreach (var media in existingPrimary)
            {
                media.IsPrimary = false;
                media.UpdatedAt = DateTime.UtcNow;
            }
        }

        entity.MediaUrl = dto.MediaUrl;
        entity.ThumbnailUrl = dto.ThumbnailUrl;
        entity.MediaType = dto.MediaType;
        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsPrimary = dto.IsPrimary;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<PropertyMedia>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return false;

        entity.IsActive = false;
        entity.IsPrimary = false;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> SetPrimaryAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<PropertyMedia>()
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);

        if (entity == null)
            return false;

        var propertyMedia = await _context.Set<PropertyMedia>()
            .Where(x =>
                x.PropertyId == entity.PropertyId &&
                x.IsActive)
            .ToListAsync(cancellationToken);

        foreach (var media in propertyMedia)
        {
            media.IsPrimary = media.Id == id;
            media.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> ReorderAsync(
        int propertyId,
        List<int> mediaIds,
        CancellationToken cancellationToken = default)
    {
        if (mediaIds == null || mediaIds.Count == 0)
            return false;

        var media = await _context.Set<PropertyMedia>()
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsActive)
            .ToListAsync(cancellationToken);

        if (media.Count == 0)
            return false;

        for (var i = 0; i < mediaIds.Count; i++)
        {
            var item = media.FirstOrDefault(x => x.Id == mediaIds[i]);

            if (item != null)
            {
                item.DisplayOrder = i;
                item.UpdatedAt = DateTime.UtcNow;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
