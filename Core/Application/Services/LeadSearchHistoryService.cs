using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadSearchHistoryService
{
    private readonly AppDbContext _context;

    public LeadSearchHistoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadSearchHistory> CreateAsync(LeadSearchHistory history)
    {
        history.CreatedAt = DateTime.UtcNow;
        history.SearchedAt = DateTime.UtcNow;
        history.IsActive = true;

        _context.LeadSearchHistories.Add(history);

        await _context.SaveChangesAsync();

        return history;
    }

    public async Task<List<LeadSearchHistory>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadSearchHistories
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.SearchedAt)
            .ToListAsync();
    }

    public async Task<LeadSearchHistory?> GetByIdAsync(int id)
    {
        return await _context.LeadSearchHistories
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadSearchHistory updated)
    {
        var history = await _context.LeadSearchHistories
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (history is null)
            return false;

        history.SearchText = updated.SearchText;
        history.PropertyType = updated.PropertyType;
        history.City = updated.City;
        history.District = updated.District;
        history.MinPrice = updated.MinPrice;
        history.MaxPrice = updated.MaxPrice;
        history.ResultsCount = updated.ResultsCount;
        history.FiltersJson = updated.FiltersJson;
        history.Notes = updated.Notes;
        history.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var history = await _context.LeadSearchHistories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (history is null)
            return false;

        history.IsActive = false;
        history.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
