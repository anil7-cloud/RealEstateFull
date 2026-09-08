using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PriceHistoryService
{
    private readonly AppDbContext _context;

    public PriceHistoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyPriceHistory> AddPriceHistory(
        Guid propertyId,
        decimal oldPrice,
        decimal newPrice,
        string changedBy)
    {
        var difference = newPrice - oldPrice;

        var percentage =
            oldPrice == 0
                ? 0
                : (difference / oldPrice) * 100;

        var history = new PropertyPriceHistory
        {
            PropertyListingId = propertyId,
            OldPrice = oldPrice,
            NewPrice = newPrice,
            Difference = difference,
            PercentageChange = percentage,
            ChangedBy = changedBy.Trim(),
            ChangedAt = DateTime.UtcNow
        };

        _context.PropertyPriceHistories.Add(history);

        await _context.SaveChangesAsync();

        return history;
    }


    public async Task<List<PropertyPriceHistory>>
        GetPropertyHistory(Guid propertyId)
    {
        return await _context.PropertyPriceHistories
            .Where(x => x.PropertyListingId == propertyId)
            .OrderByDescending(x => x.ChangedAt)
            .ToListAsync();
    }


    public async Task<PropertyPriceHistory?>
        GetLastPriceChange(Guid propertyId)
    {
        return await _context.PropertyPriceHistories
            .Where(x => x.PropertyListingId == propertyId)
            .OrderByDescending(x => x.ChangedAt)
            .FirstOrDefaultAsync();
    }


    public async Task<bool> DeleteHistory(Guid id)
    {
        var history = await _context.PropertyPriceHistories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (history is null)
            return false;

        _context.PropertyPriceHistories.Remove(history);

        await _context.SaveChangesAsync();

        return true;
    }
}
