using REAL_ESTATE_CLEAN.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace REAL_ESTATE_CLEAN.Core.Application;

public class RecommendationService
{
    private readonly AppDbContext _db;

    public RecommendationService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<object>> GetTop()
    {
        return await _db.Properties
            .OrderByDescending(x => x.IsPremium)
            .Take(10)
            .Select(x => new
            {
                x.Id,
                x.Title,
                x.Price,
                x.City
            })
            .ToListAsync<object>();
    }
}
