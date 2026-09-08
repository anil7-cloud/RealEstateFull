using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadApiRequestLogService
{
    private readonly AppDbContext _context;

    public LeadApiRequestLogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadApiRequestLog> CreateAsync(LeadApiRequestLog log)
    {
        log.RequestedAt = DateTime.UtcNow;

        _context.LeadApiRequestLogs.Add(log);

        await _context.SaveChangesAsync();

        return log;
    }

    public async Task<List<LeadApiRequestLog>> GetAllAsync()
    {
        return await _context.LeadApiRequestLogs
            .OrderByDescending(x => x.RequestedAt)
            .ToListAsync();
    }

    public async Task<LeadApiRequestLog?> GetByIdAsync(int id)
    {
        return await _context.LeadApiRequestLogs
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadApiRequestLog>> GetByApiKeyAsync(int apiKeyId)
    {
        return await _context.LeadApiRequestLogs
            .Where(x => x.LeadApiKeyId == apiKeyId)
            .OrderByDescending(x => x.RequestedAt)
            .ToListAsync();
    }

    public async Task<List<LeadApiRequestLog>> GetSuccessfulAsync()
    {
        return await _context.LeadApiRequestLogs
            .Where(x => x.IsSuccess)
            .OrderByDescending(x => x.RequestedAt)
            .ToListAsync();
    }

    public async Task<List<LeadApiRequestLog>> GetFailedAsync()
    {
        return await _context.LeadApiRequestLogs
            .Where(x => !x.IsSuccess)
            .OrderByDescending(x => x.RequestedAt)
            .ToListAsync();
    }

    public async Task<bool> CompleteAsync(
        int id,
        int statusCode,
        string responseBody,
        bool success,
        string errorMessage,
        long durationMilliseconds)
    {
        var log = await _context.LeadApiRequestLogs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (log == null)
            return false;

        log.StatusCode = statusCode;
        log.ResponseBodyJson = responseBody;
        log.IsSuccess = success;
        log.ErrorMessage = errorMessage;
        log.DurationMilliseconds = durationMilliseconds;
        log.RespondedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var log = await _context.LeadApiRequestLogs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (log == null)
            return false;

        _context.LeadApiRequestLogs.Remove(log);

        await _context.SaveChangesAsync();

        return true;
    }
}
