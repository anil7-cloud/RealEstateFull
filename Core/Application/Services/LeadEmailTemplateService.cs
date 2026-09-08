using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadEmailTemplateService
{
    private readonly AppDbContext _context;

    public LeadEmailTemplateService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadEmailTemplate> CreateAsync(
        LeadEmailTemplate template)
    {
        template.CreatedAt = DateTime.UtcNow;
        template.IsActive = true;

        _context.LeadEmailTemplates.Add(template);

        await _context.SaveChangesAsync();

        return template;
    }


    public async Task<List<LeadEmailTemplate>> GetAllAsync()
    {
        return await _context.LeadEmailTemplates
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }


    public async Task<LeadEmailTemplate?> GetByIdAsync(
        int id)
    {
        return await _context.LeadEmailTemplates
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> UpdateAsync(
        LeadEmailTemplate updated)
    {
        var template = await _context.LeadEmailTemplates
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (template == null)
            return false;


        template.Name = updated.Name;
        template.Subject = updated.Subject;
        template.HtmlBody = updated.HtmlBody;
        template.Description = updated.Description;
        template.IsActive = updated.IsActive;
        template.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        var template = await _context.LeadEmailTemplates
            .FirstOrDefaultAsync(x => x.Id == id);

        if (template == null)
            return false;


        template.IsActive = false;
        template.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }
}
