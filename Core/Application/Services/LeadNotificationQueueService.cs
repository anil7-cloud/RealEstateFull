using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadNotificationQueueService
{
    private readonly AppDbContext _context;

    public LeadNotificationQueueService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadNotificationQueue> CreateAsync(
        int leadId,
        string recipient,
        string title,
        string message)
    {
        var queue = new LeadNotificationQueue
        {
            LeadId = leadId,
            Recipient = recipient,
            Title = title,
            Message = message,
            Channel = "Push",
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadNotificationQueues.Add(queue);

        await _context.SaveChangesAsync();

        return queue;
    }


    public async Task<List<LeadNotificationQueue>> GetPendingAsync()
    {
        return await _context.LeadNotificationQueues
            .Where(x => x.Status == "Pending")
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> CompleteAsync(int id)
    {
        var item = await _context.LeadNotificationQueues
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return false;

        item.Status = "Completed";
        item.IsCompleted = true;
        item.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
