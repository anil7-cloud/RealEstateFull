using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadConversionFunnelService
{
    private readonly AppDbContext _context;

    public LeadConversionFunnelService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadConversionFunnel> CreateAsync(LeadConversionFunnel funnel)
    {
        funnel.CreatedAt = DateTime.UtcNow;
        funnel.EnteredAt = DateTime.UtcNow;
        funnel.IsActive = true;

        _context.LeadConversionFunnels.Add(funnel);

        await _context.SaveChangesAsync();

        return funnel;
    }

    public async Task<List<LeadConversionFunnel>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadConversionFunnels
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderBy(x => x.StageOrder)
            .ToListAsync();
    }

    public async Task<LeadConversionFunnel?> GetByIdAsync(int id)
    {
        return await _context.LeadConversionFunnels
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<LeadConversionFunnel?> GetCurrentStageAsync(int leadId)
    {
        return await _context.LeadConversionFunnels
            .FirstOrDefaultAsync(x =>
                x.LeadId == leadId &&
                x.IsCurrentStage &&
                x.IsActive);
    }

    public async Task<bool> UpdateAsync(LeadConversionFunnel updated)
    {
        var funnel = await _context.LeadConversionFunnels
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (funnel is null)
            return false;

        funnel.FunnelStage = updated.FunnelStage;
        funnel.StageOrder = updated.StageOrder;
        funnel.ConversionProbability = updated.ConversionProbability;
        funnel.ExitedAt = updated.ExitedAt;
        funnel.DurationInDays = updated.DurationInDays;
        funnel.IsCurrentStage = updated.IsCurrentStage;
        funnel.IsConverted = updated.IsConverted;
        funnel.ExitReason = updated.ExitReason;
        funnel.Notes = updated.Notes;
        funnel.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var funnel = await _context.LeadConversionFunnels
            .FirstOrDefaultAsync(x => x.Id == id);

        if (funnel is null)
            return false;

        funnel.IsActive = false;
        funnel.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
