using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class FloorPlanService
{
    private readonly AppDbContext _context;

    public FloorPlanService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FloorPlan> CreateFloorPlan(
        int propertyId,
        string title,
        string imageUrl,
        decimal areaSquareMeters,
        int floorNumber)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Kat planı başlığı boş olamaz.",
                nameof(title));

        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException(
                "Kat planı görseli boş olamaz.",
                nameof(imageUrl));

        if (areaSquareMeters <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(areaSquareMeters));

        var floorPlan = new FloorPlan
        {
            PropertyId = propertyId,
            Title = title.Trim(),
            ImageUrl = imageUrl.Trim(),
            AreaSquareMeters = areaSquareMeters,
            FloorNumber = floorNumber,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.FloorPlans.Add(floorPlan);
        await _context.SaveChangesAsync();

        return floorPlan;
    }

    public async Task<List<FloorPlan>> GetPropertyFloorPlans(
        int propertyId)
    {
        return await _context.FloorPlans
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsActive)
            .OrderBy(x => x.FloorNumber)
            .ThenBy(x => x.Title)
            .ToListAsync();
    }

    public async Task<bool> DeleteFloorPlan(int id)
    {
        var floorPlan = await _context.FloorPlans
            .FirstOrDefaultAsync(x => x.Id == id);

        if (floorPlan is null)
            return false;

        _context.FloorPlans.Remove(floorPlan);
        await _context.SaveChangesAsync();

        return true;
    }
}
