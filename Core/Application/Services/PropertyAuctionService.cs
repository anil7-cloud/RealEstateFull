using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyAuctionService
{
    private readonly AppDbContext _context;

    public PropertyAuctionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyAuction> CreateAuction(
        int propertyId,
        decimal startingPrice,
        DateTime startDate,
        DateTime endDate)
    {
        if (startingPrice <= 0)
            throw new ArgumentOutOfRangeException(nameof(startingPrice));

        if (endDate <= startDate)
            throw new ArgumentException(
                "Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");

        var auction = new PropertyAuction
        {
            PropertyId = propertyId,
            StartingPrice = startingPrice,
            CurrentHighestBid = startingPrice,
            StartDate = startDate,
            EndDate = endDate,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.PropertyAuctions.Add(auction);

        await _context.SaveChangesAsync();

        return auction;
    }

    public async Task<bool> PlaceBid(
        int auctionId,
        int userId,
        decimal amount)
    {
        var auction = await _context.PropertyAuctions
            .FirstOrDefaultAsync(x => x.Id == auctionId);

        if (auction is null)
            return false;

        if (!auction.IsActive)
            return false;

        if (DateTime.UtcNow > auction.EndDate)
            return false;

        if (amount <= auction.CurrentHighestBid)
            return false;

        auction.CurrentHighestBid = amount;
        auction.HighestBidUserId = userId;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<PropertyAuction>> GetActiveAuctions()
    {
        return await _context.PropertyAuctions
            .Where(x =>
                x.IsActive &&
                x.EndDate >= DateTime.UtcNow)
            .OrderBy(x => x.EndDate)
            .ToListAsync();
    }

    public async Task<bool> CloseAuction(int auctionId)
    {
        var auction = await _context.PropertyAuctions
            .FirstOrDefaultAsync(x => x.Id == auctionId);

        if (auction is null)
            return false;

        auction.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
    }
}
