using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadSmsTemplateService
{
    private readonly AppDbContext _context;

    public LeadSmsTemplateService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadSmsTemplate> CreateAsync(LeadSmsTemplate template)
    {
        template.CreatedAt = DateTime.UtcNow;

        _context.LeadSmsTemplates.Add(template);

        await _context.SaveChangesAsync();

        return template;
    }

    public async Task<List<LeadSmsTemplate>> GetAllAsync()
    {
        return await _context.LeadSmsTemplates
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<LeadSmsTemplate?> GetByIdAsync(int id)
    {
        return await _context.LeadSmsTemplates
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<LeadSmsTemplate?> GetByCodeAsync(string code)
    {
        return await _context.LeadSmsTemplates
            .FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task<List<LeadSmsTemplate>> GetActiveAsync()
    {
        return await _context.LeadSmsTemplates
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<bool> IncreaseUsageAsync(int id)
    {
        var template = await _context.LeadSmsTemplates
            .FirstOrDefaultAsync(x => x.Id == id);

        if (template == null)
            return false;

        template.UsageCount++;
        template.LastUsedAt = DateTime.UtcNow;
        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(LeadSmsTemplate updated)
    {
        var template = await _context.LeadSmsTemplates
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (template == null)
            return false;

        template.Name = updated.Name;
        template.Code = updated.Code;
        template.Message = updated.Message;
        template.Description = updated.Description;
        template.VariablesJson = updated.VariablesJson;
        template.Category = updated.Category;
        template.Language = updated.Language;
        template.IsActive = updated.IsActive;
        template.IsDefault = updated.IsDefault;
        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var template = await _context.LeadSmsTemplates
            .FirstOrDefaultAsync(x => x.Id == id);

        if (template == null)
            return false;

        _context.LeadSmsTemplates.Remove(template);

        await _context.SaveChangesAsync();

        return true;
    }
}
