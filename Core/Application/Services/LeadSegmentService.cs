using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadSegmentService
{
    private readonly AppDbContext _context;

    public LeadSegmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadSegment> CreateAsync(LeadSegment segment)
    {
        segment.CreatedAt = DateTime.UtcNow;
        segment.IsActive = true;

        _context.LeadSegments.Add(segment);

        await _context.SaveChangesAsync();

        return segment;
    }

    public async Task<List<LeadSegment>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadSegments
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.Priority)
            .ThenByDescending(x => x.Score)
            .ToListAsync();
    }

    public async Task<LeadSegment?> GetByIdAsync(int id)
    {
        return await _context.LeadSegments
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadSegment>> GetVipSegmentsAsync()
    {
        return await _context.LeadSegments
            .Where(x => x.IsActive && x.IsVip)
            .OrderByDescending(x => x.Score)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(LeadSegment updated)
    {
        var segment = await _context.LeadSegments
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (segment is null)
            return false;

        segment.SegmentName = updated.SegmentName;
        segment.Category = updated.Category;
        segment.MinBudget = updated.MinBudget;
        segment.MaxBudget = updated.MaxBudget;
        segment.Region = updated.Region;
        segment.Score = updated.Score;
        segment.Priority = updated.Priority;
        segment.IsVip = updated.IsVip;
        segment.IsQualified = updated.IsQualified;
        segment.Notes = updated.Notes;
        segment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var segment = await _context.LeadSegments
            .FirstOrDefaultAsync(x => x.Id == id);

        if (segment is null)
            return false;

        segment.IsActive = false;
        segment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
