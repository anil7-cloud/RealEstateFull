using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyReportService
{
    private readonly AppDbContext _context;

    public PropertyReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task ReportProperty(
        int propertyId,
        int userId,
        string reason)
    {
        var report = new PropertyReport
        {
            PropertyId = propertyId,
            UserId = userId,
            Reason = reason,
            CreatedAt = DateTime.UtcNow
        };

        _context.PropertyReports.Add(report);

        await _context.SaveChangesAsync();
    }

    public async Task<List<PropertyReport>> GetReports()
    {
        return await _context.PropertyReports
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<PropertyReport>> GetPropertyReports(int propertyId)
    {
        return await _context.PropertyReports
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task DeleteReport(int reportId)
    {
        var report = await _context.PropertyReports
            .FirstOrDefaultAsync(x => x.Id == reportId);

        if (report == null)
            return;

        _context.PropertyReports.Remove(report);

        await _context.SaveChangesAsync();
    }
}
