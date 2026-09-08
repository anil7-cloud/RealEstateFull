using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadWinReasonService
{
    private readonly AppDbContext _context;

    public LeadWinReasonService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadWinReason> CreateAsync(LeadWinReason winReason)
    {
        winReason.CreatedAt = DateTime.UtcNow;
        winReason.WonDate = DateTime.UtcNow;
        winReason.IsActive = true;

        _context.LeadWinReasons.Add(winReason);

        await _context.SaveChangesAsync();

        return winReason;
    }

    public async Task<List<LeadWinReason>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadWinReasons
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.WonDate)
            .ToListAsync();
    }

    public async Task<LeadWinReason?> GetByIdAsync(int id)
    {
        return await _context.LeadWinReasons
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadWinReason>> GetTopSalesAsync()
    {
        return await _context.LeadWinReasons
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.SalePrice)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(LeadWinReason updated)
    {
        var winReason = await _context.LeadWinReasons
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (winReason is null)
            return false;

        winReason.UserId = updated.UserId;
        winReason.WinCategory = updated.WinCategory;
        winReason.Reason = updated.Reason;
        winReason.SalePrice = updated.SalePrice;
        winReason.CommissionAmount = updated.CommissionAmount;
        winReason.CompetitiveAdvantage = updated.CompetitiveAdvantage;
        winReason.CustomerDecisionFactor = updated.CustomerDecisionFactor;
        winReason.SuccessStrategy = updated.SuccessStrategy;
        winReason.Notes = updated.Notes;
        winReason.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var winReason = await _context.LeadWinReasons
            .FirstOrDefaultAsync(x => x.Id == id);

        if (winReason is null)
            return false;

        winReason.IsActive = false;
        winReason.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
