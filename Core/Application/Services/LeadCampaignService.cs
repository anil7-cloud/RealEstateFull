using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadCampaignService
{
    private readonly AppDbContext _context;

    public LeadCampaignService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadCampaign> CreateAsync(
        string name,
        string platform,
        string campaignType,
        decimal budget)
    {
        var campaign = new LeadCampaign
        {
            Name = name,
            Platform = platform,
            CampaignType = campaignType,
            Budget = budget,
            StartDate = DateTime.UtcNow,
            Status = "Draft",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadCampaigns.Add(campaign);

        await _context.SaveChangesAsync();

        return campaign;
    }


    public async Task<List<LeadCampaign>> GetActiveAsync()
    {
        return await _context.LeadCampaigns
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> UpdateStatusAsync(
        int id,
        string status)
    {
        var campaign = await _context.LeadCampaigns
            .FirstOrDefaultAsync(x => x.Id == id);

        if (campaign == null)
            return false;

        campaign.Status = status;
        campaign.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
