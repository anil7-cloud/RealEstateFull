using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadReportService
{
    private readonly AppDbContext _context;

    public LeadReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadReport> CreateAsync(LeadReport report)
    {
        report.CreatedAt = DateTime.UtcNow;
        report.IsActive = true;

        _context.LeadReports.Add(report);

        await _context.SaveChangesAsync();

        return report;
    }

    public async Task<List<LeadReport>> GetAllAsync()
    {
        return await _context.LeadReports
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<LeadReport?> GetByIdAsync(int id)
    {
        return await _context.LeadReports
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadReport>> GetByCategoryAsync(string category)
    {
        return await _context.LeadReports
            .Where(x => x.Category == category && x.IsActive)
            .OrderBy(x => x.ReportName)
            .ToListAsync();
    }

    public async Task<List<LeadReport>> GetScheduledAsync()
    {
        return await _context.LeadReports
            .Where(x => x.IsScheduled && x.IsActive)
            .OrderBy(x => x.NextRunAt)
            .ToListAsync();
    }

    public async Task<bool> MarkGeneratedAsync(int id)
    {
        var report = await _context.LeadReports
            .FirstOrDefaultAsync(x => x.Id == id);

        if (report is null)
            return false;

        report.LastGeneratedAt = DateTime.UtcNow;
        report.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(LeadReport updated)
    {
        var report = await _context.LeadReports
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (report is null)
            return false;

        report.ReportName = updated.ReportName;
        report.ReportType = updated.ReportType;
        report.Category = updated.Category;
        report.FiltersJson = updated.FiltersJson;
        report.ColumnsJson = updated.ColumnsJson;
        report.SortBy = updated.SortBy;
        report.SortDescending = updated.SortDescending;
        report.IsPublic = updated.IsPublic;
        report.IsScheduled = updated.IsScheduled;
        report.ScheduleCron = updated.ScheduleCron;
        report.NextRunAt = updated.NextRunAt;
        report.ExportFormat = updated.ExportFormat;
        report.Notes = updated.Notes;
        report.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var report = await _context.LeadReports
            .FirstOrDefaultAsync(x => x.Id == id);

        if (report is null)
            return false;

        report.IsActive = false;
        report.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
