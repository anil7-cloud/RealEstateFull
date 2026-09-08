using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowExecutionScheduleService
{
    private readonly AppDbContext _context;

    public WorkflowExecutionScheduleService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<WorkflowExecutionSchedule> CreateAsync(
        Guid historyId,
        string cronExpression)
    {
        var schedule = new WorkflowExecutionSchedule
        {
            WorkflowExecutionHistoryId = historyId,
            CronExpression = cronExpression,
            IsEnabled = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.WorkflowExecutionSchedules.Add(schedule);

        await _context.SaveChangesAsync();

        return schedule;
    }


    public async Task<List<WorkflowExecutionSchedule>> GetActiveAsync()
    {
        return await _context.WorkflowExecutionSchedules
            .Where(x => x.IsEnabled)
            .OrderBy(x => x.NextRunAt)
            .ToListAsync();
    }


    public async Task<bool> UpdateNextRunAsync(
        Guid id,
        DateTime nextRun)
    {
        var schedule = await _context.WorkflowExecutionSchedules
            .FirstOrDefaultAsync(x => x.Id == id);

        if (schedule == null)
            return false;

        schedule.NextRunAt = nextRun;
        schedule.LastRunAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DisableAsync(Guid id)
    {
        var schedule = await _context.WorkflowExecutionSchedules
            .FirstOrDefaultAsync(x => x.Id == id);

        if (schedule == null)
            return false;

        schedule.IsEnabled = false;

        await _context.SaveChangesAsync();

        return true;
    }
}
