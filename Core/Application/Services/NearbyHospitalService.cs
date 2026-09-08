using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class NearbyHospitalService
{
    private readonly AppDbContext _context;

    public NearbyHospitalService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<NearbyHospital> CreateHospital(
        int propertyId,
        string name,
        string hospitalType,
        string address,
        decimal distanceKm,
        bool hasEmergencyService,
        double rating)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Hastane adı boş olamaz.",
                nameof(name));

        if (distanceKm < 0)
            throw new ArgumentOutOfRangeException(
                nameof(distanceKm));

        rating = Math.Clamp(rating, 0, 5);

        var hospital = new NearbyHospital
        {
            PropertyId = propertyId,
            Name = name.Trim(),
            HospitalType = hospitalType.Trim(),
            Address = address.Trim(),
            DistanceKm = distanceKm,
            HasEmergencyService = hasEmergencyService,
            Rating = rating,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.NearbyHospitals.Add(hospital);
        await _context.SaveChangesAsync();

        return hospital;
    }

    public async Task<List<NearbyHospital>> GetPropertyHospitals(
        int propertyId)
    {
        return await _context.NearbyHospitals
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsActive)
            .OrderBy(x => x.DistanceKm)
            .ThenByDescending(x => x.Rating)
            .ToListAsync();
    }

    public async Task<List<NearbyHospital>> GetEmergencyHospitals(
        int propertyId)
    {
        return await _context.NearbyHospitals
            .Where(x =>
                x.PropertyId == propertyId &&
                x.HasEmergencyService &&
                x.IsActive)
            .OrderBy(x => x.DistanceKm)
            .ToListAsync();
    }

    public async Task<bool> DeleteHospital(int id)
    {
        var hospital = await _context.NearbyHospitals
            .FirstOrDefaultAsync(x => x.Id == id);

        if (hospital is null)
            return false;

        hospital.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
    }
}
