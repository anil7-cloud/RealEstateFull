using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadWorkflowService
{
    private readonly AppDbContext _context;

    public LeadWorkflowService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<LeadWorkflow>> GetAllAsync()
    {
        return await _context.LeadWorkflows
            .Where(x => x.IsActive)
            .ToListAsync();
    }


    public async Task<LeadWorkflow?> GetByIdAsync(int id)
    {
        return await _context.LeadWorkflows
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<LeadWorkflow> CreateAsync(LeadWorkflow workflow)
    {
        workflow.CreatedAt = DateTime.UtcNow;
        workflow.IsActive = true;

        _context.LeadWorkflows.Add(workflow);

        await _context.SaveChangesAsync();

        return workflow;
    }


    public async Task<bool> UpdateAsync(LeadWorkflow workflow)
    {
        var item = await _context.LeadWorkflows
            .FirstOrDefaultAsync(x => x.Id == workflow.Id);

        if (item == null)
            return false;


        item.CurrentStep = workflow.CurrentStep;
        item.Status = workflow.Status;
        item.NextAction = workflow.NextAction;
        item.NextActionDate = workflow.NextActionDate;
        item.IsCompleted = workflow.IsCompleted;
        item.IsPaused = workflow.IsPaused;
        item.Notes = workflow.Notes;
        item.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.LeadWorkflows
            .FirstOrDefaultAsync(x => x.Id == id);

        if(item == null)
            return false;


        item.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
    }
}
