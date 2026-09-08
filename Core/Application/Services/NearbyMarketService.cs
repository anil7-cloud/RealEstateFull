using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class NearbyMarketService
{
    private readonly AppDbContext _context;

    public NearbyMarketService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<NearbyMarket> CreateMarket(
        int propertyId,
        string name,
        string marketType,
        string address,
        decimal distanceKm,
        bool isOpen24Hours,
        double rating)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Market adı boş olamaz.",
                nameof(name));

        if (distanceKm < 0)
            throw new ArgumentOutOfRangeException(
                nameof(distanceKm));

        var market = new NearbyMarket
        {
            PropertyId = propertyId,
            Name = name.Trim(),
            MarketType = marketType.Trim(),
            Address = address.Trim(),
            DistanceKm = distanceKm,
            IsOpen24Hours = isOpen24Hours,
            Rating = Math.Clamp(rating, 0, 5),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.NearbyMarkets.Add(market);
        await _context.SaveChangesAsync();

        return market;
    }

    public async Task<NearbyMarket?> GetMarketById(int id)
    {
        return await _context.NearbyMarkets
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<NearbyMarket>> GetPropertyMarkets(
        int propertyId)
    {
        return await _context.NearbyMarkets
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsActive)
            .OrderBy(x => x.DistanceKm)
            .ThenByDescending(x => x.Rating)
            .ToListAsync();
    }

    public async Task<List<NearbyMarket>> GetOpen24HourMarkets(
        int propertyId)
    {
        return await _context.NearbyMarkets
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsOpen24Hours &&
                x.IsActive)
            .OrderBy(x => x.DistanceKm)
            .ToListAsync();
    }

    public async Task<List<NearbyMarket>> GetMarketsWithinDistance(
        int propertyId,
        decimal maxDistanceKm)
    {
        return await _context.NearbyMarkets
            .Where(x =>
                x.PropertyId == propertyId &&
                x.DistanceKm <= maxDistanceKm &&
                x.IsActive)
            .OrderBy(x => x.DistanceKm)
            .ToListAsync();
    }

    public async Task<List<NearbyMarket>> SearchMarkets(
        string keyword)
    {
        keyword = keyword.Trim();

        return await _context.NearbyMarkets
            .Where(x =>
                x.Name.Contains(keyword) ||
                x.Address.Contains(keyword) ||
                x.MarketType.Contains(keyword))
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<int> GetMarketCount(int propertyId)
    {
        return await _context.NearbyMarkets
            .CountAsync(x =>
                x.PropertyId == propertyId &&
                x.IsActive);
    }

    public async Task<bool> UpdateMarket(
        int id,
        string name,
        string marketType,
        string address,
        decimal distanceKm,
        bool isOpen24Hours,
        double rating)
    {
        var market = await _context.NearbyMarkets
            .FirstOrDefaultAsync(x => x.Id == id);

        if (market is null)
            return false;

        market.Name = name.Trim();
        market.MarketType = marketType.Trim();
        market.Address = address.Trim();
        market.DistanceKm = distanceKm;
        market.IsOpen24Hours = isOpen24Hours;
        market.Rating = Math.Clamp(rating, 0, 5);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteMarket(int id)
    {
        var market = await _context.NearbyMarkets
            .FirstOrDefaultAsync(x => x.Id == id);

        if (market is null)
            return false;

        market.IsActive = false;
        await _context.SaveChangesAsync();

        return true;
    }
}
