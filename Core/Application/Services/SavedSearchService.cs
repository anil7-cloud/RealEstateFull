using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class SavedSearchService
{
    private readonly AppDbContext _context;

    public SavedSearchService(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveSearch(
        int userId,
        string searchName,
        string searchQuery)
    {
        var item = new SavedSearch
        {
            UserId = userId,
            Name = searchName,
            Query = searchQuery,
            CreatedAt = DateTime.UtcNow
        };

        _context.SavedSearches.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task<List<SavedSearch>> GetSavedSearches(int userId)
    {
        return await _context.SavedSearches
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task DeleteSavedSearch(int id)
    {
        var item = await _context.SavedSearches
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return;

        _context.SavedSearches.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSavedSearch(int userId, int id)
    {
        var item = await _context.SavedSearches
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.UserId == userId);

        if (item == null)
            return;

        _context.SavedSearches.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task NotifyMatchingUsers(
        Property property,
        NotificationService notificationService)
    {
        var searches = await _context.SavedSearches
            .Where(x => x.UserId > 0)
            .ToListAsync();

        foreach (var search in searches)
        {
            SavedSearchFilter? filter = null;

            if (!string.IsNullOrWhiteSpace(search.Query))
            {
                try
                {
                    filter =
                        System.Text.Json.JsonSerializer
                            .Deserialize<SavedSearchFilter>(
                                search.Query);
                }
                catch
                {
                    continue;
                }
            }

            if (filter is null)
                continue;

            if (!string.IsNullOrWhiteSpace(filter.SelectedCity) &&
                !string.Equals(
                    filter.SelectedCity.Trim(),
                    property.City.Trim(),
                    StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (filter.MinPrice.HasValue &&
                property.Price < filter.MinPrice.Value)
            {
                continue;
            }

            if (filter.MaxPrice.HasValue &&
                property.Price > filter.MaxPrice.Value)
            {
                continue;
            }

            if (!string.IsNullOrWhiteSpace(filter.PropertyType) &&
                !string.Equals(
                    filter.PropertyType.Trim(),
                    property.PropertyType.Trim(),
                    StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (!string.IsNullOrWhiteSpace(filter.RoomCount) &&
                !string.Equals(
                    filter.RoomCount.Trim(),
                    property.RoomCount.Trim(),
                    StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (!string.IsNullOrWhiteSpace(filter.Q))
            {
                var q = filter.Q.Trim();

                var matchesText =
                    property.Title.Contains(
                        q,
                        StringComparison.OrdinalIgnoreCase) ||
                    property.City.Contains(
                        q,
                        StringComparison.OrdinalIgnoreCase) ||
                    property.Location.Contains(
                        q,
                        StringComparison.OrdinalIgnoreCase);

                if (!matchesText)
                    continue;
            }

            await notificationService.AddNotification(
                search.UserId,
                $"🔎 Kaydettiğiniz aramaya uygun yeni ilan: {property.Title}");
        }
    }

    private sealed class SavedSearchFilter
    {
        public string? Q { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string? SelectedCity { get; set; }

        public string? PropertyType { get; set; }

        public string? RoomCount { get; set; }

        public string? Sort { get; set; }
    }
}
