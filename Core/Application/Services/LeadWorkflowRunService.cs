using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadWorkflowRunService
{
    private readonly AppDbContext _context;

    public LeadWorkflowRunService(AppDbContext context)
    {
        _context = context;
    }

    public async Task RunAsync(
        int workflowId,
        int leadId)
    {
        var steps = await _context.LeadWorkflowSteps
            .Where(x => x.LeadWorkflowId == workflowId)
            .OrderBy(x => x.StepOrder)
            .ToListAsync();

        foreach (var step in steps)
        {
            step.Status = "Completed";
            step.CompletedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }
}
