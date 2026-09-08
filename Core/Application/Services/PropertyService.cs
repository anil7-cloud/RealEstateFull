using REAL_ESTATE_CLEAN.Core.Application.DTO;
using REAL_ESTATE_CLEAN.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyService
{
    private readonly AppDbContext _context;
    private readonly SavedSearchService _savedSearchService;
    private readonly NotificationService _notificationService;
    private readonly SpamProtectionService _spamProtectionService;

    public PropertyService(
        AppDbContext context,
        SavedSearchService savedSearchService,
        NotificationService notificationService,
        SpamProtectionService spamProtectionService)
    {
        _context = context;
        _savedSearchService = savedSearchService;
        _notificationService = notificationService;
        _spamProtectionService = spamProtectionService;
    }

    private IQueryable<PropertyDto> BuildQuery()
    {
        return _context.Properties
            .Where(x => x.IsActive)
            .Select(x => new PropertyDto
            {
                Id = x.Id,
                Title = x.Title,
                Location = x.Location,
                City = x.City,
                District = x.District,
                Price = x.Price,
                IsPremium = x.IsPremium,

                CoverImageUrl = _context.PropertyMedia
                    .Where(m =>
                        m.PropertyId == x.Id &&
                        m.MediaType == "Image")
                    .OrderByDescending(m => m.IsPrimary)
                    .ThenBy(m => m.DisplayOrder)
                    .Select(m => m.MediaUrl)
                    .FirstOrDefault()
            });
    }

    public async Task<List<PropertyDto>> GetAll()
    {
        return await BuildQuery()
            .ToListAsync();
    }

    public async Task<PropertyDto?> GetById(int id)
    {
        return await _context.Properties
            .Where(x => x.Id == id)
            .Select(x => new PropertyDto
            {
                Id = x.Id,
                Title = x.Title,
                Location = x.Location,
                City = x.City,
                District = x.District,
                Price = x.Price,
                IsPremium = x.IsPremium,

                CoverImageUrl = _context.PropertyMedia
                    .Where(m =>
                        m.PropertyId == x.Id &&
                        m.MediaType == "Image")
                    .OrderByDescending(m => m.IsPrimary)
                    .ThenBy(m => m.DisplayOrder)
                    .Select(m => m.MediaUrl)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<PropertyDto>> Search(
        string? q = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        string? selectedCity = null,
        string? propertyType = null,
        string? roomCount = null,
        string? sort = null,
        string? district = null)
    {
        var query = _context.Properties
            .Where(x => x.IsActive)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();

            query = query.Where(x =>
                x.Title.Contains(q) ||
                x.City.Contains(q) ||
                x.Location.Contains(q));
        }

        if (minPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price <= maxPrice.Value);
        }

        if (!string.IsNullOrWhiteSpace(selectedCity))
        {
            query = query.Where(x =>
                x.City == selectedCity);
        }

        if (!string.IsNullOrWhiteSpace(propertyType))
        {
            query = query.Where(x =>
                x.PropertyType == propertyType);
        }

        if (!string.IsNullOrWhiteSpace(district))
        {
            query = query.Where(x =>
                x.District == district);
        }


        if (!string.IsNullOrWhiteSpace(roomCount))
        {
            query = query.Where(x =>
                x.RoomCount == roomCount);
        }

        sort = sort?.Trim();

        query = sort switch
        {
            "price-asc" =>
                query.OrderBy(x => x.Price),

            "price-desc" =>
                query.OrderByDescending(x => x.Price),

            "premium" =>
                query
                    .OrderByDescending(x => x.IsPremium)
                    .ThenByDescending(x => x.CreatedAt),

            _ =>
                query.OrderByDescending(x => x.CreatedAt)
        };

        return await query
            .Select(x => new PropertyDto
            {
                Id = x.Id,
                Title = x.Title,
                Location = x.Location,
                City = x.City,
                District = x.District,
                Price = x.Price,
                IsPremium = x.IsPremium,
                PropertyType = x.PropertyType,
                RoomCount = x.RoomCount,

                CoverImageUrl = _context.PropertyMedia
                    .Where(m =>
                        m.PropertyId == x.Id &&
                        m.MediaType == "Image")
                    .OrderByDescending(m => m.IsPrimary)
                    .ThenBy(m => m.DisplayOrder)
                    .Select(m => m.MediaUrl)
                    .FirstOrDefault()
            })
            .ToListAsync();
    }

    public async Task<Property> AddAsync(Property property)
    {
        if (!_spamProtectionService.IsPropertyCreationAllowed(property.AppUserId))
        {
            throw new InvalidOperationException(
                "Çok kısa sürede çok fazla ilan oluşturuldu. Lütfen daha sonra tekrar deneyin.");
        }

        property.Title = property.Title.Trim();
        property.Location = property.Location.Trim();
        property.City = property.City.Trim();
        property.District = property.District?.Trim() ?? "";

        property.IsActive = true;
        property.ViewCount = 0;
        property.CreatedAt = DateTime.UtcNow;

        _context.Properties.Add(property);

        await _context.SaveChangesAsync();

        await _savedSearchService.NotifyMatchingUsers(
            property,
            _notificationService);

        return property;
    }

    public async Task<List<PropertyDto>> GetByUserId(int userId)
    {
        return await _context.Properties
            .Where(x => x.AppUserId == userId)
            .Select(x => new PropertyDto
            {
                Id = x.Id,
                Title = x.Title,
                Location = x.Location,
                City = x.City,
                District = x.District,
                Price = x.Price,
                IsPremium = x.IsPremium,

                CoverImageUrl = _context.PropertyMedia
                    .Where(m =>
                        m.PropertyId == x.Id &&
                        m.MediaType == "Image")
                    .OrderByDescending(m => m.IsPrimary)
                    .ThenBy(m => m.DisplayOrder)
                    .Select(m => m.MediaUrl)
                    .FirstOrDefault()
            })
            .ToListAsync();
    }

    public async Task<bool> DeleteByUserId(
        int propertyId,
        int userId)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(x =>
                x.Id == propertyId &&
                x.AppUserId == userId);

        if (property is null)
            return false;

        var media = await _context.PropertyMedia
            .Where(x => x.PropertyId == propertyId)
            .ToListAsync();

        _context.PropertyMedia.RemoveRange(media);
        _context.Properties.Remove(property);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SetActiveByUserId(
        int propertyId,
        int userId,
        bool isActive)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(x =>
                x.Id == propertyId &&
                x.AppUserId == userId);

        if (property is null)
            return false;

        property.IsActive = isActive;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Property>> GetAllForAdmin()
    {
        return await _context.Properties
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> SetActiveByAdmin(
        int propertyId,
        bool isActive)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(x => x.Id == propertyId);

        if (property is null)
            return false;

        property.IsActive = isActive;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> BelongsToUser(
        int propertyId,
        int userId)
    {
        return await _context.Properties
            .AnyAsync(x =>
                x.Id == propertyId &&
                x.AppUserId == userId);
    }

    public async Task<bool> DeleteMediaByUserId(
        int mediaId,
        int userId)
    {
        var media = await _context.PropertyMedia
            .FirstOrDefaultAsync(x => x.Id == mediaId);

        if (media is null)
            return false;

        var property = await _context.Properties
            .FirstOrDefaultAsync(x =>
                x.Id == media.PropertyId &&
                x.AppUserId == userId);

        if (property is null)
            return false;

        _context.PropertyMedia.Remove(media);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SetCoverByUserId(
        int mediaId,
        int userId)
    {
        var media = await _context.PropertyMedia
            .FirstOrDefaultAsync(x => x.Id == mediaId);

        if (media is null)
            return false;

        var property = await _context.Properties
            .FirstOrDefaultAsync(x =>
                x.Id == media.PropertyId &&
                x.AppUserId == userId);

        if (property is null)
            return false;

        var propertyMedia = await _context.PropertyMedia
            .Where(x => x.PropertyId == property.Id)
            .ToListAsync();

        foreach (var item in propertyMedia)
        {
            item.IsPrimary = item.Id == mediaId;
        }

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SetCoverMediaByUserId(
        int mediaId,
        int userId)
    {
        var media = await _context.PropertyMedia
            .FirstOrDefaultAsync(x => x.Id == mediaId);

        if (media is null)
            return false;

        var property = await _context.Properties
            .FirstOrDefaultAsync(x =>
                x.Id == media.PropertyId &&
                x.AppUserId == userId);

        if (property is null)
            return false;

        var propertyMedia = await _context.PropertyMedia
            .Where(x => x.PropertyId == property.Id)
            .ToListAsync();

        foreach (var item in propertyMedia)
        {
            item.IsPrimary = item.Id == mediaId;
        }

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> UpdateByUserId(
        int propertyId,
        int userId,
        string title,
        string location,
        string city,
        decimal price,
        bool isPremium)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(x =>
                x.Id == propertyId &&
                x.AppUserId == userId);

        if (property is null)
            return false;

        property.Title = title.Trim();
        property.Location = location.Trim();
        property.City = city.Trim();
        property.Price = price;
        property.IsPremium = isPremium;

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> IncrementViewCount(int propertyId)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(x =>
                x.Id == propertyId &&
                x.IsActive);

        if (property is null)
            return false;

        property.ViewCount++;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<PropertyDto>> GetSimilarProperties(
        int propertyId,
        string city,
        decimal price)
    {
        var minPrice = price * 0.70m;
        var maxPrice = price * 1.30m;

        return await _context.Properties
            .Where(x =>
                x.IsActive &&
                x.Id != propertyId &&
                x.City == city &&
                x.Price >= minPrice &&
                x.Price <= maxPrice)
            .OrderByDescending(x => x.IsPremium)
            .ThenByDescending(x => x.CreatedAt)
            .Take(6)
            .Select(x => new PropertyDto
            {
                Id = x.Id,
                Title = x.Title,
                Location = x.Location,
                City = x.City,
                District = x.District,
                Price = x.Price,
                IsPremium = x.IsPremium,
                ViewCount = x.ViewCount,
                CoverImageUrl = _context.PropertyMedia
                    .Where(m =>
                        m.PropertyId == x.Id &&
                        m.MediaType == "Image")
                    .OrderByDescending(m => m.IsPrimary)
                    .ThenBy(m => m.DisplayOrder)
                    .Select(m => m.MediaUrl)
                    .FirstOrDefault()
            })
            .ToListAsync();
    }

}
