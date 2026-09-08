using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowExecutionCleanupService
{
    private readonly AppDbContext _context;

    public WorkflowExecutionCleanupService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<int> DeleteOldHistories(
        DateTime beforeDate)
    {
        var items = await _context.WorkflowExecutionHistories
            .Where(x => x.CreatedAt < beforeDate)
            .ToListAsync();


        if (!items.Any())
            return 0;


        _context.WorkflowExecutionHistories.RemoveRange(items);

        await _context.SaveChangesAsync();

        return items.Count;
    }


    public async Task<int> DeleteOldLogs(
        DateTime beforeDate)
    {
        var items = await _context.WorkflowExecutionLogs
            .Where(x => x.CreatedAt < beforeDate)
            .ToListAsync();


        if (!items.Any())
            return 0;


        _context.WorkflowExecutionLogs.RemoveRange(items);

        await _context.SaveChangesAsync();

        return items.Count;
    }
}
