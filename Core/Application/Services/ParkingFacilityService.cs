using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class ParkingFacilityService
{
    private readonly AppDbContext _context;

    public ParkingFacilityService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ParkingFacility> CreateParkingFacility(
        int propertyId,
        string name,
        string parkingType,
        int capacity,
        decimal distanceKm,
        decimal hourlyPrice,
        bool isCovered,
        bool isOpen24Hours)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Otopark adı boş olamaz.",
                nameof(name));

        if (capacity < 0)
            throw new ArgumentOutOfRangeException(
                nameof(capacity));

        if (distanceKm < 0)
            throw new ArgumentOutOfRangeException(
                nameof(distanceKm));

        if (hourlyPrice < 0)
            throw new ArgumentOutOfRangeException(
                nameof(hourlyPrice));

        var parkingFacility = new ParkingFacility
        {
            PropertyId = propertyId,
            Name = name.Trim(),
            ParkingType = parkingType.Trim(),
            Capacity = capacity,
            DistanceKm = distanceKm,
            HourlyPrice = hourlyPrice,
            IsCovered = isCovered,
            IsOpen24Hours = isOpen24Hours,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.ParkingFacilities.Add(parkingFacility);
        await _context.SaveChangesAsync();

        return parkingFacility;
    }

    public async Task<List<ParkingFacility>> GetPropertyParkingFacilities(
        int propertyId)
    {
        return await _context.ParkingFacilities
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsActive)
            .OrderBy(x => x.DistanceKm)
            .ThenBy(x => x.HourlyPrice)
            .ToListAsync();
    }

    public async Task<List<ParkingFacility>> GetCoveredParkingFacilities(
        int propertyId)
    {
        return await _context.ParkingFacilities
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsCovered &&
                x.IsActive)
            .OrderBy(x => x.DistanceKm)
            .ToListAsync();
    }

    public async Task<bool> DeleteParkingFacility(int id)
    {
        var parkingFacility = await _context.ParkingFacilities
            .FirstOrDefaultAsync(x => x.Id == id);

        if (parkingFacility is null)
            return false;

        parkingFacility.IsActive = false;
        await _context.SaveChangesAsync();

        return true;
    }
}
