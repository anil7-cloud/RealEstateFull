using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class CampaignService
{
    private readonly AppDbContext _context;

    public CampaignService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Campaign> CreateCampaign(
        string title,
        string description,
        decimal discountRate,
        DateTime endDate)
    {
        var campaign = new Campaign
        {
            Title = title,
            Description = description,
            DiscountRate = discountRate,
            StartDate = DateTime.UtcNow,
            EndDate = endDate,
            IsActive = true
        };

        _context.Campaigns.Add(campaign);

        await _context.SaveChangesAsync();

        return campaign;
    }
}
