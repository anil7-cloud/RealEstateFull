using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class NearbyPharmacyService
{
    private readonly AppDbContext _context;

    public NearbyPharmacyService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<NearbyPharmacy> CreatePharmacy(
        int propertyId,
        string name,
        string address,
        decimal distanceKm,
        bool isOnDuty,
        string phone)
    {
        var pharmacy = new NearbyPharmacy
        {
            PropertyId = propertyId,
            Name = name.Trim(),
            Address = address.Trim(),
            DistanceKm = distanceKm,
            IsOnDuty = isOnDuty,
            Phone = phone.Trim(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.NearbyPharmacies.Add(pharmacy);
        await _context.SaveChangesAsync();

        return pharmacy;
    }

    public async Task<List<NearbyPharmacy>> GetPropertyPharmacies(
        int propertyId)
    {
        return await _context.NearbyPharmacies
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsActive)
            .OrderBy(x => x.DistanceKm)
            .ToListAsync();
    }

    public async Task<bool> DeletePharmacy(int id)
    {
        var pharmacy = await _context.NearbyPharmacies
            .FirstOrDefaultAsync(x => x.Id == id);

        if (pharmacy is null)
            return false;

        pharmacy.IsActive = false;
        await _context.SaveChangesAsync();

        return true;
    }
}
