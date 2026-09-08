using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadWebhookHistoryService
{
    private readonly AppDbContext _context;

    public LeadWebhookHistoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadWebhookHistory> CreateAsync(LeadWebhookHistory history)
    {
        history.TriggeredAt = DateTime.UtcNow;

        _context.LeadWebhookHistories.Add(history);

        await _context.SaveChangesAsync();

        return history;
    }

    public async Task<List<LeadWebhookHistory>> GetAllAsync()
    {
        return await _context.LeadWebhookHistories
            .OrderByDescending(x => x.TriggeredAt)
            .ToListAsync();
    }

    public async Task<LeadWebhookHistory?> GetByIdAsync(int id)
    {
        return await _context.LeadWebhookHistories
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadWebhookHistory>> GetByWebhookAsync(int webhookId)
    {
        return await _context.LeadWebhookHistories
            .Where(x => x.LeadWebhookId == webhookId)
            .OrderByDescending(x => x.TriggeredAt)
            .ToListAsync();
    }

    public async Task<List<LeadWebhookHistory>> GetSuccessfulAsync()
    {
        return await _context.LeadWebhookHistories
            .Where(x => x.IsSuccess)
            .OrderByDescending(x => x.TriggeredAt)
            .ToListAsync();
    }

    public async Task<List<LeadWebhookHistory>> GetFailedAsync()
    {
        return await _context.LeadWebhookHistories
            .Where(x => !x.IsSuccess)
            .OrderByDescending(x => x.TriggeredAt)
            .ToListAsync();
    }

    public async Task<bool> CompleteAsync(
        int id,
        bool success,
        int? statusCode,
        string responseBody,
        string errorMessage,
        long durationMilliseconds)
    {
        var history = await _context.LeadWebhookHistories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (history == null)
            return false;

        history.IsSuccess = success;
        history.ResponseStatusCode = statusCode;
        history.ResponseBody = responseBody;
        history.ErrorMessage = errorMessage;
        history.DurationMilliseconds = durationMilliseconds;
        history.CompletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var history = await _context.LeadWebhookHistories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (history == null)
            return false;

        _context.LeadWebhookHistories.Remove(history);

        await _context.SaveChangesAsync();

        return true;
    }
}
