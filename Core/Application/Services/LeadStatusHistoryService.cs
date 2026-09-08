using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadStatusHistoryService
{
    private readonly AppDbContext _context;

    public LeadStatusHistoryService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<LeadStatusHistory>> GetAllAsync()
    {
        return await _context.LeadStatusHistories
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.ChangedAt)
            .ToListAsync();
    }


    public async Task<List<LeadStatusHistory>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadStatusHistories
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.ChangedAt)
            .ToListAsync();
    }


    public async Task<LeadStatusHistory> CreateAsync(LeadStatusHistory history)
    {
        history.CreatedAt = DateTime.UtcNow;
        history.ChangedAt = DateTime.UtcNow;
        history.IsActive = true;

        _context.LeadStatusHistories.Add(history);

        await _context.SaveChangesAsync();

        return history;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var history = await _context.LeadStatusHistories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (history == null)
            return false;

        history.IsActive = false;
        history.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
