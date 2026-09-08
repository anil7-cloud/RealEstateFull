using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyContractService
{
    private readonly AppDbContext _context;

    public PropertyContractService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<PropertyContract>> GetPropertyContracts(Guid propertyId)
    {
        return await _context.PropertyContracts
            .Where(x => x.PropertyListingId == propertyId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<PropertyContract?> GetContract(Guid id)
    {
        return await _context.PropertyContracts
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<PropertyContract> CreateContract(PropertyContract contract)
    {
        _context.PropertyContracts.Add(contract);

        await _context.SaveChangesAsync();

        return contract;
    }


    public async Task<bool> DeleteContract(Guid id)
    {
        var contract = await _context.PropertyContracts
            .FirstOrDefaultAsync(x => x.Id == id);

        if (contract == null)
            return false;

        _context.PropertyContracts.Remove(contract);

        await _context.SaveChangesAsync();

        return true;
    }
}
