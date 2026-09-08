using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowSchedulerService
{
    private readonly AppDbContext _context;

    public WorkflowSchedulerService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<LeadWorkflowTrigger>> GetActiveTriggers()
    {
        return await _context.LeadWorkflowTriggers
            .Where(x => x.IsActive)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> ExecuteTrigger(int id)
    {
        var trigger = await _context.LeadWorkflowTriggers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (trigger == null)
            return false;


        await _context.SaveChangesAsync();

        return true;
    }
}
