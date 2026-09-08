using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPerformanceService
{
    private readonly AppDbContext _context;

    public LeadPerformanceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadPerformance> CreateAsync(LeadPerformance performance)
    {
        performance.CreatedAt = DateTime.UtcNow;
        performance.IsActive = true;

        _context.LeadPerformances.Add(performance);

        await _context.SaveChangesAsync();

        return performance;
    }

    public async Task<List<LeadPerformance>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadPerformances
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.PerformanceScore)
            .ToListAsync();
    }

    public async Task<LeadPerformance?> GetByIdAsync(int id)
    {
        return await _context.LeadPerformances
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadPerformance>> GetTopPerformancesAsync()
    {
        return await _context.LeadPerformances
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.PerformanceScore)
            .ThenByDescending(x => x.ConversionRate)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(LeadPerformance updated)
    {
        var performance = await _context.LeadPerformances
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (performance is null)
            return false;

        performance.UserId = updated.UserId;
        performance.CallsCount = updated.CallsCount;
        performance.MeetingsCount = updated.MeetingsCount;
        performance.VisitsCount = updated.VisitsCount;
        performance.EmailsCount = updated.EmailsCount;
        performance.SmsCount = updated.SmsCount;
        performance.TasksCompleted = updated.TasksCompleted;
        performance.ConversionRate = updated.ConversionRate;
        performance.EstimatedRevenue = updated.EstimatedRevenue;
        performance.ActualRevenue = updated.ActualRevenue;
        performance.PerformanceScore = updated.PerformanceScore;
        performance.Notes = updated.Notes;
        performance.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var performance = await _context.LeadPerformances
            .FirstOrDefaultAsync(x => x.Id == id);

        if (performance is null)
            return false;

        performance.IsActive = false;
        performance.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
