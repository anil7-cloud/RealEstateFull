using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushScheduleService
{
    private readonly AppDbContext _context;

    public LeadPushScheduleService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadPushSchedule> CreateAsync(
        int campaignId,
        DateTime scheduledAt)
    {
        var schedule = new LeadPushSchedule
        {
            LeadPushCampaignId = campaignId,
            Name = "Push Schedule",
            Description = "Automatic push schedule",
            ScheduledAt = scheduledAt,
            Status = "Pending",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadPushSchedules.Add(schedule);

        await _context.SaveChangesAsync();

        return schedule;
    }


    public async Task<List<LeadPushSchedule>> GetActiveAsync()
    {
        return await _context.LeadPushSchedules
            .Where(x => x.IsActive)
            .OrderBy(x => x.ScheduledAt)
            .ToListAsync();
    }
}
