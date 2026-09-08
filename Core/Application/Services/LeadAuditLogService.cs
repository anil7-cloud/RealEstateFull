using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadAuditLogService
{
    private readonly AppDbContext _context;

    public LeadAuditLogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadAuditLog> CreateAsync(LeadAuditLog log)
    {
        log.CreatedAt = DateTime.UtcNow;

        _context.LeadAuditLogs.Add(log);

        await _context.SaveChangesAsync();

        return log;
    }

    public async Task<List<LeadAuditLog>> GetAllAsync()
    {
        return await _context.LeadAuditLogs
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<LeadAuditLog?> GetByIdAsync(int id)
    {
        return await _context.LeadAuditLogs
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadAuditLog>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadAuditLogs
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<LeadAuditLog>> GetByUserAsync(int userId)
    {
        return await _context.LeadAuditLogs
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<LeadAuditLog>> GetByActionAsync(string action)
    {
        return await _context.LeadAuditLogs
            .Where(x => x.Action == action)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<LeadAuditLog>> GetFailedLogsAsync()
    {
        return await _context.LeadAuditLogs
            .Where(x => !x.IsSuccess)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<LeadAuditLog>> GetByDateRangeAsync(
        DateTime startDate,
        DateTime endDate)
    {
        return await _context.LeadAuditLogs
            .Where(x =>
                x.CreatedAt >= startDate &&
                x.CreatedAt <= endDate)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var log = await _context.LeadAuditLogs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (log == null)
            return false;

        _context.LeadAuditLogs.Remove(log);

        await _context.SaveChangesAsync();

        return true;
    }
}
