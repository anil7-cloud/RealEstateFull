using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PublicTransportService
{
    private readonly AppDbContext _context;

    public PublicTransportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PublicTransportStop> CreateStop(
        int propertyId,
        string name,
        string transportType,
        decimal distanceKm,
        string lineNames,
        bool isAccessible)
    {
        var stop = new PublicTransportStop
        {
            PropertyId = propertyId,
            Name = name.Trim(),
            TransportType = transportType.Trim(),
            DistanceKm = distanceKm,
            LineNames = lineNames.Trim(),
            IsAccessible = isAccessible,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.PublicTransportStops.Add(stop);

        await _context.SaveChangesAsync();

        return stop;
    }

    public async Task<List<PublicTransportStop>> GetStops(
        int propertyId)
    {
        return await _context.PublicTransportStops
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsActive)
            .OrderBy(x => x.DistanceKm)
            .ToListAsync();
    }

    public async Task<List<PublicTransportStop>> GetMetroStops(
        int propertyId)
    {
        return await _context.PublicTransportStops
            .Where(x =>
                x.PropertyId == propertyId &&
                x.TransportType == "Metro" &&
                x.IsActive)
            .OrderBy(x => x.DistanceKm)
            .ToListAsync();
    }

    public async Task<bool> DeleteStop(int id)
    {
        var stop = await _context.PublicTransportStops
            .FirstOrDefaultAsync(x => x.Id == id);

        if (stop is null)
            return false;

        _context.PublicTransportStops.Remove(stop);

        await _context.SaveChangesAsync();

        return true;
    }
}
