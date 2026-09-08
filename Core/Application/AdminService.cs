using REAL_ESTATE_CLEAN.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace REAL_ESTATE_CLEAN.Core.Application;

public class AdminService
{
    private readonly AppDbContext _context;

    public AdminService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Property>> GetAll()
    {
        return await _context.Properties.ToListAsync();
    }

    public async Task<List<Property>> GetPremium()
    {
        return await _context.Properties
            .Where(x => x.IsPremium)
            .ToListAsync();
    }

    public async Task<List<Property>> GetByCity(string city)
    {
        return await _context.Properties
            .Where(x => x.City == city)
            .ToListAsync();
    }
}
