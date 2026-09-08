using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyInsuranceService
{
    private readonly AppDbContext _context;

    public PropertyInsuranceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyInsurance> CreateInsurance(
        PropertyInsurance insurance)
    {
        _context.PropertyInsurances.Add(insurance);

        await _context.SaveChangesAsync();

        return insurance;
    }

    public async Task<List<PropertyInsurance>> GetPropertyInsurances(
        int propertyId)
    {
        return await _context.PropertyInsurances
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.EndDate)
            .ToListAsync();
    }

    public async Task<List<PropertyInsurance>> GetExpiringPolicies(
        int days)
    {
        var limitDate = DateTime.UtcNow.AddDays(days);

        return await _context.PropertyInsurances
            .Where(x =>
                x.IsActive &&
                x.EndDate <= limitDate)
            .OrderBy(x => x.EndDate)
            .ToListAsync();
    }

    public async Task<bool> RenewPolicy(
        int id,
        DateTime newEndDate)
    {
        var insurance = await _context.PropertyInsurances
            .FirstOrDefaultAsync(x => x.Id == id);

        if (insurance is null)
            return false;

        insurance.EndDate = newEndDate;
        insurance.IsActive = true;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteInsurance(int id)
    {
        var insurance = await _context.PropertyInsurances
            .FirstOrDefaultAsync(x => x.Id == id);

        if (insurance is null)
            return false;

        _context.PropertyInsurances.Remove(insurance);

        await _context.SaveChangesAsync();

        return true;
    }
}
