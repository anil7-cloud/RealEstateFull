using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyVisitService
{
    private readonly AppDbContext _context;

    public PropertyVisitService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyVisit> CreateVisit(PropertyVisit visit)
    {
        _context.PropertyVisits.Add(visit);

        await _context.SaveChangesAsync();

        return visit;
    }


    public async Task<List<PropertyVisit>> GetPropertyVisits(Guid propertyId)
    {
        return await _context.PropertyVisits
            .Where(x => x.PropertyListingId == propertyId)
            .OrderBy(x => x.VisitDate)
            .ToListAsync();
    }


    public async Task<List<PropertyVisit>> GetUserVisits(Guid userId)
    {
        return await _context.PropertyVisits
            .Where(x => x.CustomerUserId == userId)
            .OrderByDescending(x => x.VisitDate)
            .ToListAsync();
    }


    public async Task<bool> UpdateVisitStatus(Guid id, string status)
    {
        var visit = await _context.PropertyVisits
            .FirstOrDefaultAsync(x => x.Id == id);

        if (visit is null)
            return false;

        visit.Status = status.Trim();

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteVisit(Guid id)
    {
        var visit = await _context.PropertyVisits
            .FirstOrDefaultAsync(x => x.Id == id);

        if (visit is null)
            return false;

        _context.PropertyVisits.Remove(visit);

        await _context.SaveChangesAsync();

        return true;
    }
}
