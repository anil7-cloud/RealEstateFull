using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadSystemLogService
{
    private readonly AppDbContext _context;

    public LeadSystemLogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadSystemLog> CreateAsync(LeadSystemLog log)
    {
        log.CreatedAt = DateTime.UtcNow;

        _context.LeadSystemLogs.Add(log);

        await _context.SaveChangesAsync();

        return log;
    }

    public async Task<List<LeadSystemLog>> GetAllAsync()
    {
        return await _context.LeadSystemLogs
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<LeadSystemLog?> GetByIdAsync(int id)
    {
        return await _context.LeadSystemLogs
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadSystemLog>> GetByLevelAsync(string level)
    {
        return await _context.LeadSystemLogs
            .Where(x => x.Level == level)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<LeadSystemLog>> GetResolvedAsync()
    {
        return await _context.LeadSystemLogs
            .Where(x => x.IsResolved)
            .OrderByDescending(x => x.ResolvedAt)
            .ToListAsync();
    }

    public async Task<List<LeadSystemLog>> GetUnresolvedAsync()
    {
        return await _context.LeadSystemLogs
            .Where(x => !x.IsResolved)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> ResolveAsync(int id, string notes)
    {
        var log = await _context.LeadSystemLogs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (log == null)
            return false;

        log.IsResolved = true;
        log.ResolvedAt = DateTime.UtcNow;
        log.ResolutionNotes = notes;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var log = await _context.LeadSystemLogs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (log == null)
            return false;

        _context.LeadSystemLogs.Remove(log);

        await _context.SaveChangesAsync();

        return true;
    }
}
