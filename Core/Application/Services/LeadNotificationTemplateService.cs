using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadNotificationTemplateService
{
    private readonly AppDbContext _context;

    public LeadNotificationTemplateService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadNotificationTemplate> CreateAsync(LeadNotificationTemplate template)
    {
        template.CreatedAt = DateTime.UtcNow;

        _context.LeadNotificationTemplates.Add(template);

        await _context.SaveChangesAsync();

        return template;
    }

    public async Task<List<LeadNotificationTemplate>> GetAllAsync()
    {
        return await _context.LeadNotificationTemplates
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<LeadNotificationTemplate?> GetByIdAsync(int id)
    {
        return await _context.LeadNotificationTemplates
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<LeadNotificationTemplate?> GetByCodeAsync(string code)
    {
        return await _context.LeadNotificationTemplates
            .FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task<List<LeadNotificationTemplate>> GetActiveAsync()
    {
        return await _context.LeadNotificationTemplates
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(LeadNotificationTemplate updated)
    {
        var template = await _context.LeadNotificationTemplates
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (template == null)
            return false;

        template.Name = updated.Name;
        template.Code = updated.Code;
        template.TitleTemplate = updated.TitleTemplate;
        template.MessageTemplate = updated.MessageTemplate;
        template.NotificationType = updated.NotificationType;
        template.Priority = updated.Priority;
        template.Icon = updated.Icon;
        template.Color = updated.Color;
        template.SendPush = updated.SendPush;
        template.SendEmail = updated.SendEmail;
        template.SendSms = updated.SendSms;
        template.DisplayDurationSeconds = updated.DisplayDurationSeconds;
        template.Description = updated.Description;
        template.VariablesJson = updated.VariablesJson;
        template.IsActive = updated.IsActive;
        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SetActiveAsync(int id, bool isActive)
    {
        var template = await _context.LeadNotificationTemplates
            .FirstOrDefaultAsync(x => x.Id == id);

        if (template == null)
            return false;

        template.IsActive = isActive;
        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var template = await _context.LeadNotificationTemplates
            .FirstOrDefaultAsync(x => x.Id == id);

        if (template == null)
            return false;

        _context.LeadNotificationTemplates.Remove(template);

        await _context.SaveChangesAsync();

        return true;
    }
}
