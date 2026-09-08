using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadTaskService
{
    private readonly AppDbContext _context;

    public LeadTaskService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<LeadTask>> GetAllAsync()
    {
        return await _context.LeadTasks
            .OrderByDescending(x => x.DueDate)
            .ToListAsync();
    }


    public async Task<List<LeadTask>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadTasks
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.DueDate)
            .ToListAsync();
    }


    public async Task<LeadTask> CreateAsync(LeadTask task)
    {
        task.CreatedAt = DateTime.UtcNow;
        task.Status = "Pending";

        _context.LeadTasks.Add(task);

        await _context.SaveChangesAsync();

        return task;
    }


    public async Task<bool> CompleteAsync(int id, string note)
    {
        var task = await _context.LeadTasks
            .FirstOrDefaultAsync(x => x.Id == id);

        if (task == null)
            return false;


        task.Status = "Completed";
        task.IsCompleted = true;
        task.CompletionNote = note;
        task.CompletedAt = DateTime.UtcNow;
        task.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.LeadTasks
            .FirstOrDefaultAsync(x => x.Id == id);

        if (task == null)
            return false;


        _context.LeadTasks.Remove(task);

        await _context.SaveChangesAsync();

        return true;
    }
}
