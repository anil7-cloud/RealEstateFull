using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushCampaignService
{
    private readonly AppDbContext _context;

    public LeadPushCampaignService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadPushCampaign> CreateAsync(
        string name,
        string description)
    {
        var campaign = new LeadPushCampaign
        {
            Name = name,
            Description = description,
            AudienceType = "All",
            AudienceFilterJson = "{}",
            SendImmediately = true,
            Status = "Draft",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadPushCampaigns.Add(campaign);

        await _context.SaveChangesAsync();

        return campaign;
    }


    public async Task<List<LeadPushCampaign>> GetActiveAsync()
    {
        return await _context.LeadPushCampaigns
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> UpdateStatusAsync(
        int id,
        string status)
    {
        var campaign = await _context.LeadPushCampaigns
            .FirstOrDefaultAsync(x => x.Id == id);

        if (campaign == null)
            return false;

        campaign.Status = status;
        campaign.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
