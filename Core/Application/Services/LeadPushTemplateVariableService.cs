using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushTemplateVariableService
{
    private readonly AppDbContext _context;

    public LeadPushTemplateVariableService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadPushTemplateVariable> CreateAsync(
        int templateId,
        string name,
        string value)
    {
        var variable = new LeadPushTemplateVariable
        {
            LeadPushTemplateId = templateId,
            Name = name,
            DefaultValue = value,
            Key = name,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadPushTemplateVariables.Add(variable);

        await _context.SaveChangesAsync();

        return variable;
    }


    public async Task<List<LeadPushTemplateVariable>> GetByTemplateAsync(
        int templateId)
    {
        return await _context.LeadPushTemplateVariables
            .Where(x => x.LeadPushTemplateId == templateId)
            .OrderBy(x => x.SortOrder)
            .ToListAsync();
    }
}
