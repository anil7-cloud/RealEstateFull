using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowExecutionHistoryService
{
    private readonly AppDbContext _context;

    public WorkflowExecutionHistoryService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<WorkflowExecutionHistory> CreateAsync(
        Guid workflowId,
        Guid userId,
        string action,
        string? notes = null)
    {
        var history = new WorkflowExecutionHistory
        {
            WorkflowId = workflowId,
            UserId = userId,
            Action = action,
            Notes = notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.WorkflowExecutionHistories.Add(history);

        await _context.SaveChangesAsync();

        return history;
    }


    public async Task<List<WorkflowExecutionHistory>> GetByWorkflowAsync(
        Guid workflowId)
    {
        return await _context.WorkflowExecutionHistories
            .Where(x => x.WorkflowId == workflowId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<WorkflowExecutionHistory?> GetByIdAsync(
        Guid id)
    {
        return await _context.WorkflowExecutionHistories
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> UpdateStatusAsync(
        Guid id,
        string status)
    {
        var history = await _context.WorkflowExecutionHistories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (history == null)
            return false;

        history.Status = status;
        history.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(Guid id)
    {
        var history = await _context.WorkflowExecutionHistories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (history == null)
            return false;

        _context.WorkflowExecutionHistories.Remove(history);

        await _context.SaveChangesAsync();

        return true;
    }
}
