using REAL_ESTATE_CLEAN.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace REAL_ESTATE_CLEAN.Core.Application;

public class AdminDashboardService
{
    private readonly AppDbContext _db;

    public AdminDashboardService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<int> GetUserCount()
    {
        return await _db.Users.CountAsync();
    }

    public async Task<int> GetPropertyCount()
    {
        return await _db.Properties.CountAsync();
    }

    public async Task<int> GetTotalViews()
    {
        return await _db.PropertyViews.CountAsync();
    }
}
