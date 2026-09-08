using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadTimelineService
{
    private readonly AppDbContext _context;

    public LeadTimelineService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadTimeline> CreateAsync(
        LeadTimeline timeline)
    {
        timeline.CreatedAt = DateTime.UtcNow;

        _context.LeadTimelines.Add(timeline);

        await _context.SaveChangesAsync();

        return timeline;
    }

    public async Task<List<LeadTimeline>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadTimelines
            .Where(x =>
                x.LeadId == leadId &&
                x.IsVisible)
            .OrderByDescending(x => x.EventDate)
            .ToListAsync();
    }

    public async Task<LeadTimeline?> GetByIdAsync(int id)
    {
        return await _context.LeadTimelines
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(
        int id,
        string title,
        string description)
    {
        var timeline = await _context.LeadTimelines
            .FirstOrDefaultAsync(x => x.Id == id);

        if (timeline is null)
            return false;

        timeline.Title = title;
        timeline.Description = description;
        timeline.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> HideAsync(int id)
    {
        var timeline = await _context.LeadTimelines
            .FirstOrDefaultAsync(x => x.Id == id);

        if (timeline is null)
            return false;

        timeline.IsVisible = false;
        timeline.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var timeline = await _context.LeadTimelines
            .FirstOrDefaultAsync(x => x.Id == id);

        if (timeline is null)
            return false;

        _context.LeadTimelines.Remove(timeline);

        await _context.SaveChangesAsync();

        return true;
    }
}
