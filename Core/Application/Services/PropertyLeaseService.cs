using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyLeaseService
{
    private readonly AppDbContext _context;

    public PropertyLeaseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyLease> CreateLease(
        PropertyLease lease)
    {
        _context.PropertyLeases.Add(lease);

        await _context.SaveChangesAsync();

        return lease;
    }

    public async Task<List<PropertyLease>> GetPropertyLeases(
        int propertyId)
    {
        return await _context.PropertyLeases
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.StartDate)
            .ToListAsync();
    }

    public async Task<PropertyLease?> GetActiveLease(
        int propertyId)
    {
        return await _context.PropertyLeases
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsActive)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> TerminateLease(int id)
    {
        var lease = await _context.PropertyLeases
            .FirstOrDefaultAsync(x => x.Id == id);

        if (lease is null)
            return false;

        lease.IsActive = false;
        lease.EndDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteLease(int id)
    {
        var lease = await _context.PropertyLeases
            .FirstOrDefaultAsync(x => x.Id == id);

        if (lease is null)
            return false;

        _context.PropertyLeases.Remove(lease);

        await _context.SaveChangesAsync();

        return true;
    }
}
