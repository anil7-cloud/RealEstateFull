using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyMaintenanceScheduleService
{
    private readonly AppDbContext _context;

    public PropertyMaintenanceScheduleService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyMaintenanceSchedule> CreateSchedule(
        PropertyMaintenanceSchedule schedule)
    {
        _context.PropertyMaintenanceSchedules.Add(schedule);

        await _context.SaveChangesAsync();

        return schedule;
    }

    public async Task<List<PropertyMaintenanceSchedule>> GetSchedules(
        int propertyId)
    {
        return await _context.PropertyMaintenanceSchedules
            .Where(x => x.PropertyId == propertyId)
            .OrderBy(x => x.ScheduledDate)
            .ToListAsync();
    }

    public async Task<List<PropertyMaintenanceSchedule>> GetUpcomingSchedules()
    {
        return await _context.PropertyMaintenanceSchedules
            .Where(x =>
                x.Status == "Planned" &&
                x.ScheduledDate >= DateTime.UtcNow)
            .OrderBy(x => x.ScheduledDate)
            .ToListAsync();
    }

    public async Task<bool> CompleteSchedule(
        int id,
        decimal actualCost)
    {
        var schedule = await _context.PropertyMaintenanceSchedules
            .FirstOrDefaultAsync(x => x.Id == id);

        if (schedule is null)
            return false;

        schedule.Status = "Completed";
        schedule.CompletedDate = DateTime.UtcNow;
        schedule.ActualCost = actualCost;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteSchedule(int id)
    {
        var schedule = await _context.PropertyMaintenanceSchedules
            .FirstOrDefaultAsync(x => x.Id == id);

        if (schedule is null)
            return false;

        _context.PropertyMaintenanceSchedules.Remove(schedule);

        await _context.SaveChangesAsync();

        return true;
    }
}
