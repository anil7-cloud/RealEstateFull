using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class CrimeStatisticsService
{
    private readonly AppDbContext _context;

    public CrimeStatisticsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CrimeStatistic> CreateStatistic(
        int propertyId,
        string regionName,
        int theftCases,
        int assaultCases,
        int trafficAccidents,
        double safetyScore,
        int year)
    {
        var statistic = new CrimeStatistic
        {
            PropertyId = propertyId,
            RegionName = regionName.Trim(),
            TheftCases = theftCases,
            AssaultCases = assaultCases,
            TrafficAccidents = trafficAccidents,
            SafetyScore = Math.Clamp(safetyScore, 0, 10),
            Year = year,
            CreatedAt = DateTime.UtcNow
        };

        _context.CrimeStatistics.Add(statistic);

        await _context.SaveChangesAsync();

        return statistic;
    }

    public async Task<List<CrimeStatistic>> GetStatistics(
        int propertyId)
    {
        return await _context.CrimeStatistics
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.Year)
            .ToListAsync();
    }

    public async Task<CrimeStatistic?> GetLatestStatistic(
        int propertyId)
    {
        return await _context.CrimeStatistics
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.Year)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteStatistic(int id)
    {
        var statistic = await _context.CrimeStatistics
            .FirstOrDefaultAsync(x => x.Id == id);

        if (statistic is null)
            return false;

        _context.CrimeStatistics.Remove(statistic);

        await _context.SaveChangesAsync();

        return true;
    }
}
