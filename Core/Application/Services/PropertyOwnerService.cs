using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyOwnerService
{
    private readonly AppDbContext _context;

    public PropertyOwnerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyOwner> CreateOwner(
        PropertyOwner owner)
    {
        _context.PropertyOwners.Add(owner);

        await _context.SaveChangesAsync();

        return owner;
    }

    public async Task<List<PropertyOwner>> GetOwners()
    {
        return await _context.PropertyOwners
            .OrderBy(x => x.FullName)
            .ToListAsync();
    }

    public async Task<PropertyOwner?> GetOwner(int id)
    {
        return await _context.PropertyOwners
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateOwner(PropertyOwner owner)
    {
        var existing = await _context.PropertyOwners
            .FirstOrDefaultAsync(x => x.Id == owner.Id);

        if (existing is null)
            return false;

        existing.FullName = owner.FullName;
        existing.PhoneNumber = owner.PhoneNumber;
        existing.Email = owner.Email;
        existing.NationalId = owner.NationalId;
        existing.Address = owner.Address;
        existing.IsActive = owner.IsActive;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteOwner(int id)
    {
        var owner = await _context.PropertyOwners
            .FirstOrDefaultAsync(x => x.Id == id);

        if (owner is null)
            return false;

        _context.PropertyOwners.Remove(owner);

        await _context.SaveChangesAsync();

        return true;
    }
}
