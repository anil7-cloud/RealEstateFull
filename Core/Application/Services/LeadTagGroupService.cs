using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadTagGroupService
{
    private readonly AppDbContext _context;

    public LeadTagGroupService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadTagGroup> CreateAsync(LeadTagGroup tagGroup)
    {
        tagGroup.CreatedAt = DateTime.UtcNow;
        tagGroup.IsActive = true;

        _context.LeadTagGroups.Add(tagGroup);

        await _context.SaveChangesAsync();

        return tagGroup;
    }

    public async Task<List<LeadTagGroup>> GetAllAsync()
    {
        return await _context.LeadTagGroups
            .Where(x => x.IsActive)
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<LeadTagGroup?> GetByIdAsync(int id)
    {
        return await _context.LeadTagGroups
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadTagGroup updated)
    {
        var tagGroup = await _context.LeadTagGroups
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (tagGroup is null)
            return false;

        tagGroup.Name = updated.Name;
        tagGroup.Description = updated.Description;
        tagGroup.Color = updated.Color;
        tagGroup.SortOrder = updated.SortOrder;
        tagGroup.IsDefault = updated.IsDefault;
        tagGroup.IsSystem = updated.IsSystem;
        tagGroup.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var tagGroup = await _context.LeadTagGroups
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tagGroup is null)
            return false;

        tagGroup.IsActive = false;
        tagGroup.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
