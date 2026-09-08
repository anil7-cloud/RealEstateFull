using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushNotificationTemplateService
{
    private readonly AppDbContext _context;

    public LeadPushNotificationTemplateService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadPushNotificationTemplate> CreateAsync(LeadPushNotificationTemplate template)
    {
        template.CreatedAt = DateTime.UtcNow;

        _context.LeadPushNotificationTemplates.Add(template);

        await _context.SaveChangesAsync();

        return template;
    }

    public async Task<List<LeadPushNotificationTemplate>> GetAllAsync()
    {
        return await _context.LeadPushNotificationTemplates
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<LeadPushNotificationTemplate?> GetByIdAsync(int id)
    {
        return await _context.LeadPushNotificationTemplates
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<LeadPushNotificationTemplate?> GetByCodeAsync(string code)
    {
        return await _context.LeadPushNotificationTemplates
            .FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task<List<LeadPushNotificationTemplate>> GetActiveAsync()
    {
        return await _context.LeadPushNotificationTemplates
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<bool> IncreaseUsageAsync(int id)
    {
        var template = await _context.LeadPushNotificationTemplates
            .FirstOrDefaultAsync(x => x.Id == id);

        if (template == null)
            return false;

        template.UsageCount++;
        template.LastUsedAt = DateTime.UtcNow;
        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(LeadPushNotificationTemplate updated)
    {
        var template = await _context.LeadPushNotificationTemplates
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (template == null)
            return false;

        template.Name = updated.Name;
        template.Code = updated.Code;
        template.Title = updated.Title;
        template.Body = updated.Body;
        template.ImageUrl = updated.ImageUrl;
        template.IconUrl = updated.IconUrl;
        template.ClickAction = updated.ClickAction;
        template.DeepLink = updated.DeepLink;
        template.Sound = updated.Sound;
        template.Priority = updated.Priority;
        template.Category = updated.Category;
        template.VariablesJson = updated.VariablesJson;
        template.IsActive = updated.IsActive;
        template.IsDefault = updated.IsDefault;
        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var template = await _context.LeadPushNotificationTemplates
            .FirstOrDefaultAsync(x => x.Id == id);

        if (template == null)
            return false;

        _context.LeadPushNotificationTemplates.Remove(template);

        await _context.SaveChangesAsync();

        return true;
    }
}
