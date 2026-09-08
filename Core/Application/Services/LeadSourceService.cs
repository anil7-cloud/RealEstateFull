using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadSourceService
{
    private readonly AppDbContext _context;

    public LeadSourceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadSource> CreateAsync(LeadSource source)
    {
        source.CreatedAt = DateTime.UtcNow;
        source.IsActive = true;

        _context.LeadSources.Add(source);

        await _context.SaveChangesAsync();

        return source;
    }

    public async Task<List<LeadSource>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadSources
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.SourceDate)
            .ToListAsync();
    }

    public async Task<LeadSource?> GetByIdAsync(int id)
    {
        return await _context.LeadSources
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadSource updated)
    {
        var source = await _context.LeadSources
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (source is null)
            return false;

        source.SourceName = updated.SourceName;
        source.CampaignName = updated.CampaignName;
        source.Medium = updated.Medium;
        source.Channel = updated.Channel;
        source.AcquisitionCost = updated.AcquisitionCost;
        source.ExpectedRevenue = updated.ExpectedRevenue;
        source.Converted = updated.Converted;
        source.SourceDate = updated.SourceDate;
        source.Notes = updated.Notes;
        source.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var source = await _context.LeadSources
            .FirstOrDefaultAsync(x => x.Id == id);

        if (source is null)
            return false;

        source.IsActive = false;
        source.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
