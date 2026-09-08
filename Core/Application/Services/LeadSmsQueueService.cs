using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadSmsQueueService
{
    private readonly AppDbContext _context;

    public LeadSmsQueueService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadSmsQueue> CreateAsync(LeadSmsQueue queue)
    {
        queue.CreatedAt = DateTime.UtcNow;
        queue.Status = "Pending";

        _context.LeadSmsQueues.Add(queue);

        await _context.SaveChangesAsync();

        return queue;
    }

    public async Task<List<LeadSmsQueue>> GetAllAsync()
    {
        return await _context.LeadSmsQueues
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<LeadSmsQueue?> GetByIdAsync(int id)
    {
        return await _context.LeadSmsQueues
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadSmsQueue>> GetPendingAsync()
    {
        return await _context.LeadSmsQueues
            .Where(x => x.Status == "Pending" && !x.IsCompleted)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<LeadSmsQueue>> GetFailedAsync()
    {
        return await _context.LeadSmsQueues
            .Where(x => x.Status == "Failed")
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> MarkAsSentAsync(
        int id,
        string providerMessageId,
        string providerResponse)
    {
        var queue = await _context.LeadSmsQueues
            .FirstOrDefaultAsync(x => x.Id == id);

        if (queue == null)
            return false;

        queue.Status = "Sent";
        queue.IsCompleted = true;
        queue.ProviderMessageId = providerMessageId;
        queue.ProviderResponse = providerResponse;
        queue.SentAt = DateTime.UtcNow;
        queue.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> MarkAsFailedAsync(
        int id,
        string errorMessage)
    {
        var queue = await _context.LeadSmsQueues
            .FirstOrDefaultAsync(x => x.Id == id);

        if (queue == null)
            return false;

        queue.Status = "Failed";
        queue.ErrorMessage = errorMessage;
        queue.RetryCount++;
        queue.NextRetryAt = DateTime.UtcNow.AddMinutes(10);
        queue.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var queue = await _context.LeadSmsQueues
            .FirstOrDefaultAsync(x => x.Id == id);

        if (queue == null)
            return false;

        _context.LeadSmsQueues.Remove(queue);

        await _context.SaveChangesAsync();

        return true;
    }
}
