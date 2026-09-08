using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadRequirementService
{
    private readonly AppDbContext _context;

    public LeadRequirementService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadRequirement> CreateAsync(LeadRequirement requirement)
    {
        requirement.CreatedAt = DateTime.UtcNow;
        requirement.IsActive = true;

        _context.LeadRequirements.Add(requirement);

        await _context.SaveChangesAsync();

        return requirement;
    }

    public async Task<List<LeadRequirement>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadRequirements
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.Priority)
            .ThenBy(x => x.RequirementName)
            .ToListAsync();
    }

    public async Task<LeadRequirement?> GetByIdAsync(int id)
    {
        return await _context.LeadRequirements
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadRequirement updated)
    {
        var requirement = await _context.LeadRequirements
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (requirement is null)
            return false;

        requirement.RequirementName = updated.RequirementName;
        requirement.RequirementValue = updated.RequirementValue;
        requirement.Category = updated.Category;
        requirement.IsMandatory = updated.IsMandatory;
        requirement.Priority = updated.Priority;
        requirement.Notes = updated.Notes;
        requirement.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var requirement = await _context.LeadRequirements
            .FirstOrDefaultAsync(x => x.Id == id);

        if (requirement is null)
            return false;

        requirement.IsActive = false;
        requirement.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
