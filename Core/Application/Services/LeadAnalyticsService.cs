using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadAnalyticsService
{
    private readonly AppDbContext _context;

    public LeadAnalyticsService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<LeadAnalytics>> GetAllAsync()
    {
        return await _context.LeadAnalytics
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.CalculatedAt)
            .ToListAsync();
    }


    public async Task<List<LeadAnalytics>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadAnalytics
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.CalculatedAt)
            .ToListAsync();
    }


    public async Task<LeadAnalytics> CreateAsync(LeadAnalytics analytics)
    {
        analytics.CreatedAt = DateTime.UtcNow;
        analytics.CalculatedAt = DateTime.UtcNow;
        analytics.IsActive = true;

        _context.LeadAnalytics.Add(analytics);

        await _context.SaveChangesAsync();

        return analytics;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var analytics = await _context.LeadAnalytics
            .FirstOrDefaultAsync(x => x.Id == id);

        if (analytics == null)
            return false;

        analytics.IsActive = false;
        analytics.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
