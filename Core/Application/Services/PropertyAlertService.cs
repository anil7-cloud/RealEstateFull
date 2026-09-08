using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyAlertService
{
    private readonly AppDbContext _context;

    public PropertyAlertService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyAlert> CreateAlert(
        Guid userId,
        string alertName,
        string? city,
        string? district,
        decimal? minPrice,
        decimal? maxPrice,
        string? propertyType)
    {
        var alert = new PropertyAlert
        {
            UserId = userId,
            AlertName = alertName?.Trim() ?? string.Empty,
            City = string.IsNullOrWhiteSpace(city) ? null : city.Trim(),
            District = string.IsNullOrWhiteSpace(district) ? null : district.Trim(),
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            PropertyType = string.IsNullOrWhiteSpace(propertyType)
                ? null
                : propertyType.Trim(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.PropertyAlerts.Add(alert);
        await _context.SaveChangesAsync();

        return alert;
    }

    public async Task<List<PropertyAlert>> GetUserAlerts(Guid userId)
    {
        return await _context.PropertyAlerts
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> DeleteAlert(Guid userId, Guid alertId)
    {
        var alert = await _context.PropertyAlerts
            .FirstOrDefaultAsync(x =>
                x.Id == alertId &&
                x.UserId == userId);

        if (alert is null)
            return false;

        _context.PropertyAlerts.Remove(alert);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SetActive(
        Guid userId,
        Guid alertId,
        bool isActive)
    {
        var alert = await _context.PropertyAlerts
            .FirstOrDefaultAsync(x =>
                x.Id == alertId &&
                x.UserId == userId);

        if (alert is null)
            return false;

        alert.IsActive = isActive;

        await _context.SaveChangesAsync();

        return true;
    }
}
