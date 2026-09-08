using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyFraudReportService
{
    private readonly AppDbContext _context;

    public PropertyFraudReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyFraudReport> CreateReport(
        int propertyId,
        int reporterUserId,
        string reason,
        string description)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException(
                "Şikâyet nedeni boş olamaz.",
                nameof(reason));

        var report = new PropertyFraudReport
        {
            PropertyId = propertyId,
            ReporterUserId = reporterUserId,
            Reason = reason.Trim(),
            Description = description.Trim(),
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        _context.PropertyFraudReports.Add(report);
        await _context.SaveChangesAsync();

        return report;
    }

    public async Task<List<PropertyFraudReport>> GetPendingReports()
    {
        return await _context.PropertyFraudReports
            .Where(x => x.Status == "Pending")
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<PropertyFraudReport>> GetPropertyReports(
        int propertyId)
    {
        return await _context.PropertyFraudReports
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> ReviewReport(
        int reportId,
        string status)
    {
        var report = await _context.PropertyFraudReports
            .FirstOrDefaultAsync(x => x.Id == reportId);

        if (report is null)
            return false;

        report.Status = status.Trim();
        report.ReviewedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteReport(int reportId)
    {
        var report = await _context.PropertyFraudReports
            .FirstOrDefaultAsync(x => x.Id == reportId);

        if (report is null)
            return false;

        _context.PropertyFraudReports.Remove(report);
        await _context.SaveChangesAsync();

        return true;
    }
}
