using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadActivityService
{
    private readonly AppDbContext _context;

    public LeadActivityService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<LeadActivity>> GetAllAsync()
    {
        return await _context.LeadActivities
            .OrderByDescending(x => x.ActivityDate)
            .ToListAsync();
    }


    public async Task<List<LeadActivity>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadActivities
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.ActivityDate)
            .ToListAsync();
    }


    public async Task<LeadActivity> CreateAsync(LeadActivity activity)
    {
        activity.CreatedAt = DateTime.UtcNow;
        activity.ActivityDate = DateTime.UtcNow;

        _context.LeadActivities.Add(activity);

        await _context.SaveChangesAsync();

        return activity;
    }


    public async Task<bool> CompleteAsync(int id, string result)
    {
        var activity = await _context.LeadActivities
            .FirstOrDefaultAsync(x => x.Id == id);

        if (activity == null)
            return false;


        activity.Result = result;
        activity.IsCompleted = true;
        activity.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var activity = await _context.LeadActivities
            .FirstOrDefaultAsync(x => x.Id == id);

        if (activity == null)
            return false;


        _context.LeadActivities.Remove(activity);

        await _context.SaveChangesAsync();

        return true;
    }
}
