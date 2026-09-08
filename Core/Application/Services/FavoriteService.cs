using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Application.DTO;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class FavoriteService
{
    private readonly AppDbContext _context;

    public FavoriteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddFavorite(int userId, int propertyId)
    {
        var exists = await _context.Favorites.AnyAsync(x =>
            x.UserId == userId &&
            x.PropertyId == propertyId);

        if (exists)
            return;

        _context.Favorites.Add(new Favorite
        {
            UserId = userId,
            PropertyId = propertyId
        });

        await _context.SaveChangesAsync();
    }

    public async Task ToggleFavorite(int userId, int propertyId)
    {
        var favorite = await _context.Favorites.FirstOrDefaultAsync(x =>
            x.UserId == userId &&
            x.PropertyId == propertyId);

        if (favorite == null)
        {
            _context.Favorites.Add(new Favorite
            {
                UserId = userId,
                PropertyId = propertyId
            });
        }
        else
        {
            _context.Favorites.Remove(favorite);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<Property>> GetUserFavorites(int userId)
    {
        return await _context.Favorites
            .Where(favorite => favorite.UserId == userId)
            .Join(
                _context.Properties,
                favorite => favorite.PropertyId,
                property => property.Id,
                (_, property) => property)
            .ToListAsync();
    }

    public async Task<bool> IsFavorite(int userId, int propertyId)
    {
        return await _context.Favorites.AnyAsync(x =>
            x.UserId == userId &&
            x.PropertyId == propertyId);
    }

    public async Task<List<PropertyDto>> GetUserFavoritePropertyDtos(int userId)
    {
        return await _context.Favorites
            .Where(f => f.UserId == userId)
            .Join(
                _context.Properties,
                f => f.PropertyId,
                p => p.Id,
                (f, p) => p)
            .Where(p => p.IsActive)
            .Select(p => new PropertyDto
            {
                Id = p.Id,
                Title = p.Title,
                Location = p.Location,
                City = p.City,
                Price = p.Price,
                IsPremium = p.IsPremium,
                ViewCount = p.ViewCount,
                CoverImageUrl = _context.PropertyMedia
                    .Where(m =>
                        m.PropertyId == p.Id &&
                        m.MediaType == "Image")
                    .OrderByDescending(m => m.IsPrimary)
                    .ThenBy(m => m.DisplayOrder)
                    .Select(m => m.MediaUrl)
                    .FirstOrDefault()
            })
            .ToListAsync();
    }


    public async Task<List<int>> GetUserFavoriteIds(int userId)
    {
        return await _context.Favorites
            .Where(x => x.UserId == userId)
            .Select(x => x.PropertyId)
            .ToListAsync();
    }
}
