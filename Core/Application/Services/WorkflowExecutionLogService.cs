using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowExecutionLogService
{
    private readonly AppDbContext _context;

    public WorkflowExecutionLogService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<WorkflowExecutionLog> CreateAsync(
        Guid stepId,
        string message,
        string level = "Information",
        string? exception = null)
    {
        var log = new WorkflowExecutionLog
        {
            WorkflowExecutionStepId = stepId,
            Level = level,
            Message = message,
            Exception = exception,
            CreatedAt = DateTime.UtcNow
        };

        _context.WorkflowExecutionLogs.Add(log);

        await _context.SaveChangesAsync();

        return log;
    }


    public async Task<List<WorkflowExecutionLog>> GetByStepAsync(
        Guid stepId)
    {
        return await _context.WorkflowExecutionLogs
            .Where(x => x.WorkflowExecutionStepId == stepId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> DeleteAsync(Guid id)
    {
        var log = await _context.WorkflowExecutionLogs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (log == null)
            return false;

        _context.WorkflowExecutionLogs.Remove(log);

        await _context.SaveChangesAsync();

        return true;
    }
}
