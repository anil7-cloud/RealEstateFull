using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class VirtualTourService
{
    private readonly AppDbContext _context;

    public VirtualTourService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<VirtualTour> CreateTour(
        int propertyId,
        string provider,
        string tourUrl,
        string thumbnailUrl)
    {
        if (string.IsNullOrWhiteSpace(tourUrl))
            throw new ArgumentException(
                "Sanal tur bağlantısı boş olamaz.",
                nameof(tourUrl));

        var tour = new VirtualTour
        {
            PropertyId = propertyId,
            Provider = provider.Trim(),
            TourUrl = tourUrl.Trim(),
            ThumbnailUrl = thumbnailUrl.Trim(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.VirtualTours.Add(tour);
        await _context.SaveChangesAsync();

        return tour;
    }

    public async Task<List<VirtualTour>> GetPropertyTours(int propertyId)
    {
        return await _context.VirtualTours
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> DeleteTour(int id)
    {
        var tour = await _context.VirtualTours.FindAsync(id);

        if (tour is null)
            return false;

        _context.VirtualTours.Remove(tour);
        await _context.SaveChangesAsync();

        return true;
    }
}
