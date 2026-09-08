using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadReportTemplateService
{
    private readonly AppDbContext _context;

    public LeadReportTemplateService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadReportTemplate> CreateAsync(LeadReportTemplate template)
    {
        template.CreatedAt = DateTime.UtcNow;
        template.IsActive = true;

        _context.LeadReportTemplates.Add(template);

        await _context.SaveChangesAsync();

        return template;
    }

    public async Task<List<LeadReportTemplate>> GetAllAsync()
    {
        return await _context.LeadReportTemplates
            .Where(x => x.IsActive)
            .OrderBy(x => x.TemplateName)
            .ToListAsync();
    }

    public async Task<LeadReportTemplate?> GetByIdAsync(int id)
    {
        return await _context.LeadReportTemplates
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<LeadReportTemplate?> GetByCodeAsync(string templateCode)
    {
        return await _context.LeadReportTemplates
            .FirstOrDefaultAsync(x =>
                x.TemplateCode == templateCode &&
                x.IsActive);
    }

    public async Task<List<LeadReportTemplate>> GetByCategoryAsync(string category)
    {
        return await _context.LeadReportTemplates
            .Where(x =>
                x.Category == category &&
                x.IsActive)
            .OrderBy(x => x.TemplateName)
            .ToListAsync();
    }

    public async Task<bool> MarkAsUsedAsync(int id)
    {
        var template = await _context.LeadReportTemplates
            .FirstOrDefaultAsync(x => x.Id == id);

        if (template is null)
            return false;

        template.UsageCount++;
        template.LastUsedAt = DateTime.UtcNow;
        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(LeadReportTemplate updated)
    {
        var template = await _context.LeadReportTemplates
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (template is null)
            return false;

        template.TemplateName = updated.TemplateName;
        template.TemplateCode = updated.TemplateCode;
        template.ReportType = updated.ReportType;
        template.Category = updated.Category;
        template.LayoutJson = updated.LayoutJson;
        template.FiltersJson = updated.FiltersJson;
        template.ColumnsJson = updated.ColumnsJson;
        template.ExportFormat = updated.ExportFormat;
        template.IsDefault = updated.IsDefault;
        template.IsPublic = updated.IsPublic;
        template.Notes = updated.Notes;
        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var template = await _context.LeadReportTemplates
            .FirstOrDefaultAsync(x => x.Id == id);

        if (template is null)
            return false;

        template.IsActive = false;
        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
