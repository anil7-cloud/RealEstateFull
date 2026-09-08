using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPreferenceService
{
    private readonly AppDbContext _context;

    public LeadPreferenceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadPreference> CreateAsync(LeadPreference preference)
    {
        preference.CreatedAt = DateTime.UtcNow;
        preference.IsActive = true;

        _context.LeadPreferences.Add(preference);

        await _context.SaveChangesAsync();

        return preference;
    }

    public async Task<List<LeadPreference>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadPreferences
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<LeadPreference?> GetByIdAsync(int id)
    {
        return await _context.LeadPreferences
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadPreference updated)
    {
        var preference = await _context.LeadPreferences
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (preference is null)
            return false;

        preference.PropertyType = updated.PropertyType;
        preference.City = updated.City;
        preference.District = updated.District;
        preference.Neighborhood = updated.Neighborhood;
        preference.MinPrice = updated.MinPrice;
        preference.MaxPrice = updated.MaxPrice;
        preference.MinRoomCount = updated.MinRoomCount;
        preference.MaxRoomCount = updated.MaxRoomCount;
        preference.MinSquareMeters = updated.MinSquareMeters;
        preference.MaxSquareMeters = updated.MaxSquareMeters;
        preference.Furnished = updated.Furnished;
        preference.HasParking = updated.HasParking;
        preference.HasElevator = updated.HasElevator;
        preference.AllowsPets = updated.AllowsPets;
        preference.Notes = updated.Notes;
        preference.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var preference = await _context.LeadPreferences
            .FirstOrDefaultAsync(x => x.Id == id);

        if (preference is null)
            return false;

        preference.IsActive = false;
        preference.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
