using REAL_ESTATE_CLEAN.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace REAL_ESTATE_CLEAN.Core.Application;

public class AnalyticsEngine
{
    private readonly AppDbContext _db;

    public AnalyticsEngine(AppDbContext db)
    {
        _db = db;
    }

    public async Task<object> GetStats()
    {
        var users = await _db.Users.CountAsync();
        var properties = await _db.Properties.CountAsync();
        var views = await _db.PropertyViews.CountAsync();

        return new
        {
            Users = users,
            Properties = properties,
            Views = views
        };
    }
}
