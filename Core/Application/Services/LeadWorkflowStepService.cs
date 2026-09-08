using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadWorkflowStepService
{
    private readonly AppDbContext _context;

    public LeadWorkflowStepService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadWorkflowStep> CreateAsync(
        int workflowId,
        string stepName,
        int order)
    {
        var step = new LeadWorkflowStep
        {
            LeadWorkflowId = workflowId,
            StepName = stepName,
            StepOrder = order,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadWorkflowSteps.Add(step);

        await _context.SaveChangesAsync();

        return step;
    }


    public async Task<List<LeadWorkflowStep>> GetByWorkflowAsync(
        int workflowId)
    {
        return await _context.LeadWorkflowSteps
            .Where(x => x.LeadWorkflowId == workflowId)
            .OrderBy(x => x.StepOrder)
            .ToListAsync();
    }


    public async Task<LeadWorkflowStep?> GetByIdAsync(
        int id)
    {
        return await _context.LeadWorkflowSteps
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> CompleteAsync(
        int id)
    {
        var step = await _context.LeadWorkflowSteps
            .FirstOrDefaultAsync(x => x.Id == id);

        if (step == null)
            return false;

        step.Status = "Completed";
        step.CompletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        var step = await _context.LeadWorkflowSteps
            .FirstOrDefaultAsync(x => x.Id == id);

        if (step == null)
            return false;

        _context.LeadWorkflowSteps.Remove(step);

        await _context.SaveChangesAsync();

        return true;
    }
}
