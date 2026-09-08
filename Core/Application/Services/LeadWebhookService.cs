
using Microsoft.EntityFrameworkCore;

using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadWebhookService

{

    private readonly AppDbContext _context;

    public LeadWebhookService(AppDbContext context)

    {

        _context = context;

    }

    public async Task<LeadWebhook> CreateAsync(LeadWebhook webhook)

    {

        webhook.CreatedAt = DateTime.UtcNow;

        webhook.IsEnabled = true;

        _context.LeadWebhooks.Add(webhook);

        await _context.SaveChangesAsync();

        return webhook;

    }

    public async Task<List<LeadWebhook>> GetAllAsync()

    {

        return await _context.LeadWebhooks

            .OrderBy(x => x.Name)

            .ToListAsync();

    }

    public async Task<LeadWebhook?> GetByIdAsync(int id)

    {

        return await _context.LeadWebhooks

            .FirstOrDefaultAsync(x => x.Id == id);

    }

    public async Task<List<LeadWebhook>> GetEnabledAsync()

    {

        return await _context.LeadWebhooks

            .Where(x => x.IsEnabled)

            .OrderBy(x => x.Name)

            .ToListAsync();

    }

    public async Task<List<LeadWebhook>> GetByEventAsync(string eventName)

    {

        return await _context.LeadWebhooks

            .Where(x => x.EventName == eventName)

            .OrderBy(x => x.Name)

            .ToListAsync();

    }

    public async Task<bool> MarkExecutedAsync(

        int id,

        bool success,

        int? statusCode,

        string response)

    {

        var webhook = await _context.LeadWebhooks

            .FirstOrDefaultAsync(x => x.Id == id);

        if (webhook == null)

            return false;

        webhook.LastTriggeredAt = DateTime.UtcNow;

        webhook.LastSucceeded = success;

        webhook.LastStatusCode = statusCode;

        webhook.LastResponse = response;

        webhook.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;

    }

    public async Task<bool> UpdateAsync(LeadWebhook updated)

    {

        var webhook = await _context.LeadWebhooks

            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (webhook == null)

            return false;

        webhook.Name = updated.Name;

        webhook.EventName = updated.EventName;

        webhook.Url = updated.Url;

        webhook.HttpMethod = updated.HttpMethod;

        webhook.HeadersJson = updated.HeadersJson;

        webhook.SecretKey = updated.SecretKey;

        webhook.PayloadTemplateJson = updated.PayloadTemplateJson;

        webhook.IsEnabled = updated.IsEnabled;

        webhook.RetryCount = updated.RetryCount;

        webhook.TimeoutSeconds = updated.TimeoutSeconds;

        webhook.Description = updated.Description;

        webhook.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;

    }

    public async Task<bool> DeleteAsync(int id)

    {

        var webhook = await _context.LeadWebhooks

            .FirstOrDefaultAsync(x => x.Id == id);

        if (webhook == null)

            return false;

        _context.LeadWebhooks.Remove(webhook);

        await _context.SaveChangesAsync();

        return true;

    }

}

