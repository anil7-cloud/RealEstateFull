using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadImportTemplateService
{
    private readonly AppDbContext _context;

    public LeadImportTemplateService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadImportTemplate> CreateAsync(LeadImportTemplate template)
    {
        template.CreatedAt = DateTime.UtcNow;
        template.IsActive = true;

        _context.LeadImportTemplates.Add(template);
        await _context.SaveChangesAsync();

        return template;
    }


    public async Task<List<LeadImportTemplate>> GetAllAsync()
    {
        return await _context.LeadImportTemplates
            .Where(x => x.IsActive)
            .OrderBy(x => x.TemplateName)
            .ToListAsync();
    }


    public async Task<LeadImportTemplate?> GetByIdAsync(int id)
    {
        return await _context.LeadImportTemplates
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<LeadImportTemplate?> GetByCodeAsync(string code)
    {
        return await _context.LeadImportTemplates
            .FirstOrDefaultAsync(x => x.TemplateName == code && x.IsActive);
    }


    public async Task<List<LeadImportTemplate>> GetDefaultsAsync()
    {
        return await _context.LeadImportTemplates
            .Where(x => x.IsDefault && x.IsActive)
            .ToListAsync();
    }


    public async Task<bool> MarkAsUsedAsync(int id)
    {
        var template = await _context.LeadImportTemplates
            .FirstOrDefaultAsync(x => x.Id == id);

        if(template == null)
            return false;

        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> UpdateAsync(LeadImportTemplate updated)
    {
        var template = await _context.LeadImportTemplates
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if(template == null)
            return false;

        template.TemplateName = updated.TemplateName;
        template.Description = updated.Description;
        template.MappingJson = updated.MappingJson;
        template.IsDefault = updated.IsDefault;
        template.IsActive = updated.IsActive;
        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var template = await _context.LeadImportTemplates
            .FirstOrDefaultAsync(x => x.Id == id);

        if(template == null)
            return false;

        template.IsActive = false;
        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
