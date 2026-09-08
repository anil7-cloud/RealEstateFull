using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadSourceDetailService
{
    private readonly AppDbContext _context;

    public LeadSourceDetailService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadSourceDetail> CreateAsync(LeadSourceDetail sourceDetail)
    {
        sourceDetail.CreatedAt = DateTime.UtcNow;
        sourceDetail.SourceDate = DateTime.UtcNow;
        sourceDetail.IsActive = true;

        _context.LeadSourceDetails.Add(sourceDetail);

        await _context.SaveChangesAsync();

        return sourceDetail;
    }

    public async Task<List<LeadSourceDetail>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadSourceDetails
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.SourceDate)
            .ToListAsync();
    }

    public async Task<LeadSourceDetail?> GetByIdAsync(int id)
    {
        return await _context.LeadSourceDetails
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadSourceDetail>> GetConvertedAsync()
    {
        return await _context.LeadSourceDetails
            .Where(x => x.IsActive && x.IsConverted)
            .OrderByDescending(x => x.SourceDate)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(LeadSourceDetail updated)
    {
        var sourceDetail = await _context.LeadSourceDetails
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (sourceDetail is null)
            return false;

        sourceDetail.SourceName = updated.SourceName;
        sourceDetail.CampaignName = updated.CampaignName;
        sourceDetail.Medium = updated.Medium;
        sourceDetail.Channel = updated.Channel;
        sourceDetail.Referrer = updated.Referrer;
        sourceDetail.Device = updated.Device;
        sourceDetail.Browser = updated.Browser;
        sourceDetail.OperatingSystem = updated.OperatingSystem;
        sourceDetail.Country = updated.Country;
        sourceDetail.City = updated.City;
        sourceDetail.IpAddress = updated.IpAddress;
        sourceDetail.AcquisitionCost = updated.AcquisitionCost;
        sourceDetail.IsConverted = updated.IsConverted;
        sourceDetail.Notes = updated.Notes;
        sourceDetail.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sourceDetail = await _context.LeadSourceDetails
            .FirstOrDefaultAsync(x => x.Id == id);

        if (sourceDetail is null)
            return false;

        sourceDetail.IsActive = false;
        sourceDetail.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
