using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadEmailQueueService
{
    private readonly AppDbContext _context;

    public LeadEmailQueueService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadEmailQueue> CreateAsync(
        LeadEmailQueue queue)
    {
        queue.CreatedAt = DateTime.UtcNow;
        queue.Status = "Pending";

        _context.LeadEmailQueues.Add(queue);

        await _context.SaveChangesAsync();

        return queue;
    }


    public async Task<List<LeadEmailQueue>> GetPendingAsync()
    {
        return await _context.LeadEmailQueues
            .Where(x => x.Status == "Pending")
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<LeadEmailQueue>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadEmailQueues
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> ProcessAsync(
        int id)
    {
        var queue = await _context.LeadEmailQueues
            .FirstOrDefaultAsync(x => x.Id == id);

        if (queue == null)
            return false;


        queue.Status = "Processed";
        queue.SentAt = DateTime.UtcNow;
        queue.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> FailAsync(
        int id,
        string error)
    {
        var queue = await _context.LeadEmailQueues
            .FirstOrDefaultAsync(x => x.Id == id);

        if (queue == null)
            return false;


        queue.Status = "Failed";
        queue.ErrorMessage = error;
        queue.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        var queue = await _context.LeadEmailQueues
            .FirstOrDefaultAsync(x => x.Id == id);

        if (queue == null)
            return false;


        _context.LeadEmailQueues.Remove(queue);

        await _context.SaveChangesAsync();

        return true;
    }
}
