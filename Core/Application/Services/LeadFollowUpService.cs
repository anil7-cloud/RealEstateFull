using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadFollowUpService
{
    private readonly AppDbContext _context;

    public LeadFollowUpService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadFollowUp> CreateAsync(
        LeadFollowUp followUp)
    {
        followUp.CreatedAt = DateTime.UtcNow;
        followUp.Status = "Pending";

        _context.LeadFollowUps.Add(followUp);

        await _context.SaveChangesAsync();

        return followUp;
    }


    public async Task<List<LeadFollowUp>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadFollowUps
            .Where(x => x.LeadId == leadId)
            .OrderBy(x => x.ScheduledAt)
            .ToListAsync();
    }


    public async Task<LeadFollowUp?> GetByIdAsync(
        int id)
    {
        return await _context.LeadFollowUps
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> CompleteAsync(
        int id)
    {
        var followUp = await _context.LeadFollowUps
            .FirstOrDefaultAsync(x => x.Id == id);

        if (followUp == null)
            return false;


        followUp.Status = "Completed";
        followUp.CompletedAt = DateTime.UtcNow;
        followUp.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> CancelAsync(
        int id)
    {
        var followUp = await _context.LeadFollowUps
            .FirstOrDefaultAsync(x => x.Id == id);

        if (followUp == null)
            return false;


        followUp.Status = "Cancelled";
        followUp.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        var followUp = await _context.LeadFollowUps
            .FirstOrDefaultAsync(x => x.Id == id);

        if (followUp == null)
            return false;


        _context.LeadFollowUps.Remove(followUp);

        await _context.SaveChangesAsync();

        return true;
    }
}
