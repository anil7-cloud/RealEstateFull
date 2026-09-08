using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyRenovationService
{
    private readonly AppDbContext _context;

    public PropertyRenovationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyRenovation> CreateRenovation(
        PropertyRenovation renovation)
    {
        _context.PropertyRenovations.Add(renovation);

        await _context.SaveChangesAsync();

        return renovation;
    }

    public async Task<List<PropertyRenovation>> GetPropertyRenovations(
        int propertyId)
    {
        return await _context.PropertyRenovations
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.StartDate)
            .ToListAsync();
    }

    public async Task<List<PropertyRenovation>> GetActiveRenovations()
    {
        return await _context.PropertyRenovations
            .Where(x => x.Status == "In Progress")
            .OrderBy(x => x.StartDate)
            .ToListAsync();
    }

    public async Task<bool> CompleteRenovation(
        int id,
        decimal actualCost)
    {
        var renovation = await _context.PropertyRenovations
            .FirstOrDefaultAsync(x => x.Id == id);

        if (renovation is null)
            return false;

        renovation.Status = "Completed";
        renovation.ActualCost = actualCost;
        renovation.EndDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteRenovation(int id)
    {
        var renovation = await _context.PropertyRenovations
            .FirstOrDefaultAsync(x => x.Id == id);

        if (renovation is null)
            return false;

        _context.PropertyRenovations.Remove(renovation);

        await _context.SaveChangesAsync();

        return true;
    }
}
