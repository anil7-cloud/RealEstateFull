using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadActivityReportService
{
    private readonly AppDbContext _context;

    public LeadActivityReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadActivityReport> CreateAsync(LeadActivityReport report)
    {
        report.CreatedAt = DateTime.UtcNow;
        report.ReportDate = DateTime.UtcNow;
        report.IsActive = true;

        _context.LeadActivityReports.Add(report);

        await _context.SaveChangesAsync();

        return report;
    }

    public async Task<List<LeadActivityReport>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadActivityReports
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.ReportDate)
            .ToListAsync();
    }

    public async Task<LeadActivityReport?> GetByIdAsync(int id)
    {
        return await _context.LeadActivityReports
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadActivityReport>> GetByUserAsync(int userId)
    {
        return await _context.LeadActivityReports
            .Where(x => x.UserId == userId && x.IsActive)
            .OrderByDescending(x => x.ReportDate)
            .ToListAsync();
    }

    public async Task<List<LeadActivityReport>> GetTopPerformanceAsync()
    {
        return await _context.LeadActivityReports
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.PerformanceScore)
            .ThenByDescending(x => x.ConversionRate)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(LeadActivityReport updated)
    {
        var report = await _context.LeadActivityReports
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (report is null)
            return false;

        report.TotalCalls = updated.TotalCalls;
        report.TotalMeetings = updated.TotalMeetings;
        report.TotalEmails = updated.TotalEmails;
        report.TotalSms = updated.TotalSms;
        report.TotalVisits = updated.TotalVisits;
        report.TotalTasksCompleted = updated.TotalTasksCompleted;
        report.ConversionRate = updated.ConversionRate;
        report.RevenueGenerated = updated.RevenueGenerated;
        report.PerformanceScore = updated.PerformanceScore;
        report.Summary = updated.Summary;
        report.Notes = updated.Notes;
        report.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var report = await _context.LeadActivityReports
            .FirstOrDefaultAsync(x => x.Id == id);

        if (report is null)
            return false;

        report.IsActive = false;
        report.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
