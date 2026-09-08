using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadWorkflowTriggerService
{
    private readonly AppDbContext _context;

    public LeadWorkflowTriggerService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadWorkflowTrigger> CreateAsync(
        int workflowId,
        string triggerType)
    {
        var trigger = new LeadWorkflowTrigger
        {
            LeadWorkflowId = workflowId,
            TriggerType = triggerType,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadWorkflowTriggers.Add(trigger);

        await _context.SaveChangesAsync();

        return trigger;
    }


    public async Task<List<LeadWorkflowTrigger>> GetActiveAsync()
    {
        return await _context.LeadWorkflowTriggers
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> DisableAsync(int id)
    {
        var trigger = await _context.LeadWorkflowTriggers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (trigger == null)
            return false;

        trigger.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
    }
}
