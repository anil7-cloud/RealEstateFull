using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushCampaignRecipientService
{
    private readonly AppDbContext _context;

    public LeadPushCampaignRecipientService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadPushCampaignRecipient> CreateAsync(
        int campaignId,
        int leadId)
    {
        var recipient = new LeadPushCampaignRecipient
        {
            LeadPushCampaignId = campaignId,
            LeadId = leadId,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadPushCampaignRecipients.Add(recipient);

        await _context.SaveChangesAsync();

        return recipient;
    }


    public async Task<List<LeadPushCampaignRecipient>> GetByCampaignAsync(
        int campaignId)
    {
        return await _context.LeadPushCampaignRecipients
            .Where(x => x.LeadPushCampaignId == campaignId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> UpdateStatusAsync(
        int id,
        string status)
    {
        var recipient = await _context.LeadPushCampaignRecipients
            .FirstOrDefaultAsync(x => x.Id == id);

        if (recipient == null)
            return false;

        recipient.Status = status;
        recipient.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
