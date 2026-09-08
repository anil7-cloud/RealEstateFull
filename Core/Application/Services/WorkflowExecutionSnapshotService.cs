using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowExecutionSnapshotService
{
    private readonly AppDbContext _context;

    public WorkflowExecutionSnapshotService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<WorkflowExecutionSnapshot> CreateAsync(
        Guid historyId,
        Guid stepId,
        string snapshotName,
        string snapshotDataJson,
        string? description = null)
    {
        var snapshot = new WorkflowExecutionSnapshot
        {
            WorkflowExecutionHistoryId = historyId,
            WorkflowExecutionStepId = stepId,
            SnapshotName = snapshotName,
            SnapshotDataJson = snapshotDataJson,
            Description = description,
            CreatedAt = DateTime.UtcNow
        };

        _context.WorkflowExecutionSnapshots.Add(snapshot);

        await _context.SaveChangesAsync();

        return snapshot;
    }


    public async Task<List<WorkflowExecutionSnapshot>> GetByHistoryAsync(
        Guid historyId)
    {
        return await _context.WorkflowExecutionSnapshots
            .Where(x => x.WorkflowExecutionHistoryId == historyId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<WorkflowExecutionSnapshot?> GetByIdAsync(
        Guid id)
    {
        return await _context.WorkflowExecutionSnapshots
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> DeleteAsync(Guid id)
    {
        var snapshot = await _context.WorkflowExecutionSnapshots
            .FirstOrDefaultAsync(x => x.Id == id);

        if (snapshot == null)
            return false;

        _context.WorkflowExecutionSnapshots.Remove(snapshot);

        await _context.SaveChangesAsync();

        return true;
    }
}
