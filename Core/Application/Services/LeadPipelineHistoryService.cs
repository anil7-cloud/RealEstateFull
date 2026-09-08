using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPipelineHistoryService
{
    private readonly AppDbContext _context;

    public LeadPipelineHistoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadPipelineHistory> CreateAsync(LeadPipelineHistory history)
    {
        history.CreatedAt = DateTime.UtcNow;
        history.ChangedAt = DateTime.UtcNow;
        history.IsActive = true;

        _context.LeadPipelineHistories.Add(history);

        await _context.SaveChangesAsync();

        return history;
    }

    public async Task<List<LeadPipelineHistory>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadPipelineHistories
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.ChangedAt)
            .ToListAsync();
    }

    public async Task<LeadPipelineHistory?> GetByIdAsync(int id)
    {
        return await _context.LeadPipelineHistories
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadPipelineHistory updated)
    {
        var history = await _context.LeadPipelineHistories
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (history is null)
            return false;

        history.PreviousStage = updated.PreviousStage;
        history.CurrentStage = updated.CurrentStage;
        history.ChangedByUserId = updated.ChangedByUserId;
        history.ChangeReason = updated.ChangeReason;
        history.EstimatedValue = updated.EstimatedValue;
        history.Probability = updated.Probability;
        history.Notes = updated.Notes;
        history.IsAutomatic = updated.IsAutomatic;
        history.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var history = await _context.LeadPipelineHistories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (history is null)
            return false;

        history.IsActive = false;
        history.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
