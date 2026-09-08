using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadMatchService
{
    private readonly AppDbContext _context;

    public LeadMatchService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadMatch> CreateAsync(LeadMatch match)
    {
        match.CreatedAt = DateTime.UtcNow;
        match.IsActive = true;

        _context.LeadMatches.Add(match);

        await _context.SaveChangesAsync();

        return match;
    }

    public async Task<List<LeadMatch>> GetByLeadAsync(int leadId)
    {
        var rows = await _context.LeadMatches
            .AsNoTracking()
            .Where(x =>
                x.LeadId == leadId &&
                x.IsActive)
            .ToListAsync();

        return rows
            .OrderByDescending(x => x.MatchScore)
            .ThenByDescending(x => x.UpdatedAt ?? x.CreatedAt)
            .ToList();
    }

    public async Task<List<LeadMatch>> GetByPropertyAsync(int propertyId)
    {
        var rows = await _context.LeadMatches
            .AsNoTracking()
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsActive)
            .ToListAsync();

        return rows
            .OrderByDescending(x => x.MatchScore)
            .ThenByDescending(x => x.UpdatedAt ?? x.CreatedAt)
            .ToList();
    }

    public async Task<LeadMatch?> GetByIdAsync(int id)
    {
        return await _context.LeadMatches
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadMatch updated)
    {
        var match = await _context.LeadMatches
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (match == null)
            return false;

        match.MatchScore = updated.MatchScore;
        match.MatchLevel = updated.MatchLevel;
        match.IsRecommended = updated.IsRecommended;
        match.IsViewed = updated.IsViewed;
        match.IsContacted = updated.IsContacted;
        match.Notes = updated.Notes;
        match.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var match = await _context.LeadMatches
            .FirstOrDefaultAsync(x => x.Id == id);

        if (match == null)
            return false;

        match.IsActive = false;
        match.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
