using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadNotificationTemplateVariableService
{
    private readonly AppDbContext _context;

    public LeadNotificationTemplateVariableService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadNotificationTemplateVariable> CreateAsync(
        int templateId,
        string key,
        string value)
    {
        var variable = new LeadNotificationTemplateVariable
        {
            TemplateId = templateId,
            Key = key,
            Value = value,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadNotificationTemplateVariables.Add(variable);

        await _context.SaveChangesAsync();

        return variable;
    }


    public async Task<List<LeadNotificationTemplateVariable>> GetByTemplateAsync(
        int templateId)
    {
        return await _context.LeadNotificationTemplateVariables
            .Where(x => x.TemplateId == templateId)
            .OrderBy(x => x.Key)
            .ToListAsync();
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        var variable = await _context.LeadNotificationTemplateVariables
            .FirstOrDefaultAsync(x => x.Id == id);

        if (variable == null)
            return false;

        _context.LeadNotificationTemplateVariables.Remove(variable);

        await _context.SaveChangesAsync();

        return true;
    }
}
