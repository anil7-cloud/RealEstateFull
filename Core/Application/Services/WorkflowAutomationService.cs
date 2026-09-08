using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowAutomationService
{
    private readonly AppDbContext _context;

    public WorkflowAutomationService(AppDbContext context)
    {
        _context = context;
    }


    public async Task ExecuteWorkflowAsync(
        int workflowId,
        int leadId)
    {
        var steps = await _context.LeadWorkflowSteps
            .Where(x => x.LeadWorkflowId == workflowId)
            .OrderBy(x => x.StepOrder)
            .ToListAsync();


        foreach (var step in steps)
        {
            step.Status = "Running";

            await _context.SaveChangesAsync();


            await Task.Delay(100);


            step.Status = "Completed";
            step.CompletedAt = DateTime.UtcNow;
        }


        await _context.SaveChangesAsync();
    }


    public async Task<List<LeadWorkflowStep>> GetPendingStepsAsync(
        int workflowId)
    {
        return await _context.LeadWorkflowSteps
            .Where(x =>
                x.LeadWorkflowId == workflowId &&
                x.Status == "Pending")
            .OrderBy(x => x.StepOrder)
            .ToListAsync();
    }
}
