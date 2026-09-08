using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadWorkflowExecutionService
{
    private readonly AppDbContext _context;

    public LeadWorkflowExecutionService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadWorkflowExecution> CreateAsync(
        int leadId,
        int workflowId)
    {
        var execution = new LeadWorkflowExecution
        {
            LeadId = leadId,
            LeadWorkflowId = workflowId,
            Status = "Running",
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadWorkflowExecutions.Add(execution);

        await _context.SaveChangesAsync();

        return execution;
    }


    public async Task<List<LeadWorkflowExecution>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadWorkflowExecutions
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> CompleteAsync(int id)
    {
        var execution = await _context.LeadWorkflowExecutions
            .FirstOrDefaultAsync(x => x.Id == id);

        if (execution == null)
            return false;

        execution.Status = "Completed";
        execution.CompletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> FailAsync(
        int id,
        string error)
    {
        var execution = await _context.LeadWorkflowExecutions
            .FirstOrDefaultAsync(x => x.Id == id);

        if (execution == null)
            return false;

        execution.Status = "Failed";
        execution.ErrorMessage = error;

        await _context.SaveChangesAsync();

        return true;
    }
}
