using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadConversionService
{
    private readonly AppDbContext _context;

    public LeadConversionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadConversion> CreateAsync(LeadConversion conversion)
    {
        conversion.CreatedAt = DateTime.UtcNow;
        conversion.IsActive = true;

        _context.LeadConversions.Add(conversion);

        await _context.SaveChangesAsync();

        return conversion;
    }

    public async Task<List<LeadConversion>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadConversions
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.ConversionDate)
            .ToListAsync();
    }

    public async Task<LeadConversion?> GetByIdAsync(int id)
    {
        return await _context.LeadConversions
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadConversion updated)
    {
        var conversion = await _context.LeadConversions
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (conversion is null)
            return false;

        conversion.FromStage = updated.FromStage;
        conversion.ToStage = updated.ToStage;
        conversion.ExpectedValue = updated.ExpectedValue;
        conversion.FinalValue = updated.FinalValue;
        conversion.IsWon = updated.IsWon;
        conversion.LostReason = updated.LostReason;
        conversion.ConversionDate = updated.ConversionDate;
        conversion.Notes = updated.Notes;
        conversion.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var conversion = await _context.LeadConversions
            .FirstOrDefaultAsync(x => x.Id == id);

        if (conversion is null)
            return false;

        conversion.IsActive = false;
        conversion.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
