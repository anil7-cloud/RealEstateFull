using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadActivityLogService
{
    private readonly AppDbContext _context;

    public LeadActivityLogService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadActivityLog> CreateAsync(
        int leadId,
        string action,
        string entityName,
        string notes)
    {
        var activity = new LeadActivityLog
        {
            LeadId = leadId,
            Action = action,
            EntityName = entityName,
            Notes = notes,
            ActivityDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadActivityLogs.Add(activity);

        await _context.SaveChangesAsync();

        return activity;
    }


    public async Task<List<LeadActivityLog>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadActivityLogs
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.ActivityDate)
            .ToListAsync();
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        var activity = await _context.LeadActivityLogs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (activity == null)
            return false;

        _context.LeadActivityLogs.Remove(activity);

        await _context.SaveChangesAsync();

        return true;
    }
}
