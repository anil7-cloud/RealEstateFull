using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadTagService
{
    private readonly AppDbContext _context;

    public LeadTagService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadTag> CreateAsync(LeadTag tag)
    {
        tag.CreatedAt = DateTime.UtcNow;
        tag.IsActive = true;

        _context.LeadTags.Add(tag);

        await _context.SaveChangesAsync();

        return tag;
    }

    public async Task<List<LeadTag>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadTags
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<LeadTag?> GetByIdAsync(int id)
    {
        return await _context.LeadTags
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(
        int id,
        string name,
        string color,
        string description)
    {
        var tag = await _context.LeadTags
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tag is null)
            return false;

        tag.Name = name;
        tag.Color = color;
        tag.Description = description;
        tag.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var tag = await _context.LeadTags
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tag is null)
            return false;

        tag.IsActive = false;
        tag.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
