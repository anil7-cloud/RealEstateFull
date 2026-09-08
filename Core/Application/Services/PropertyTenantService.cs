using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyTenantService
{
    private readonly AppDbContext _context;

    public PropertyTenantService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyTenant> CreateTenant(
        PropertyTenant tenant)
    {
        _context.PropertyTenants.Add(tenant);

        await _context.SaveChangesAsync();

        return tenant;
    }

    public async Task<List<PropertyTenant>> GetPropertyTenants(
        int propertyId)
    {
        return await _context.PropertyTenants
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.MoveInDate)
            .ToListAsync();
    }

    public async Task<List<PropertyTenant>> GetActiveTenants()
    {
        return await _context.PropertyTenants
            .Where(x => x.IsActive)
            .OrderBy(x => x.FullName)
            .ToListAsync();
    }

    public async Task<bool> MoveOutTenant(
        int id,
        DateTime moveOutDate)
    {
        var tenant = await _context.PropertyTenants
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tenant is null)
            return false;

        tenant.IsActive = false;
        tenant.MoveOutDate = moveOutDate;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteTenant(int id)
    {
        var tenant = await _context.PropertyTenants
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tenant is null)
            return false;

        _context.PropertyTenants.Remove(tenant);

        await _context.SaveChangesAsync();

        return true;
    }
}
