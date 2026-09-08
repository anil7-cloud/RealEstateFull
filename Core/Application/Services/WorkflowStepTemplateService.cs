using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowStepTemplateService
{
    private readonly AppDbContext _context;

    public WorkflowStepTemplateService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<WorkflowExecutionStep> CreateAsync(
        Guid historyId,
        string name,
        int stepOrder)
    {
        var step = new WorkflowExecutionStep
        {
            WorkflowExecutionHistoryId = historyId,
            Name = name,
            StepOrder = stepOrder,
            Status = "Pending",
            StartedAt = DateTime.UtcNow
        };

        _context.WorkflowExecutionSteps.Add(step);

        await _context.SaveChangesAsync();

        return step;
    }


    public async Task<List<WorkflowExecutionStep>> GetByHistoryAsync(
        Guid historyId)
    {
        return await _context.WorkflowExecutionSteps
            .Where(x => x.WorkflowExecutionHistoryId == historyId)
            .OrderBy(x => x.StepOrder)
            .ToListAsync();
    }


    public async Task<bool> CompleteAsync(
        Guid id,
        string result)
    {
        var step = await _context.WorkflowExecutionSteps
            .FirstOrDefaultAsync(x => x.Id == id);

        if (step == null)
            return false;

        step.Status = "Completed";
        step.Result = result;
        step.CompletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
