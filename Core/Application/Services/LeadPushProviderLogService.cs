using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushProviderLogService
{
    private readonly AppDbContext _context;

    public LeadPushProviderLogService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadPushProviderLog> CreateAsync(
        int providerId,
        string request,
        string response)
    {
        var log = new LeadPushProviderLog
        {
            LeadPushProviderId = providerId,
            RequestPayload = request,
            ResponsePayload = response,
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadPushProviderLogs.Add(log);

        await _context.SaveChangesAsync();

        return log;
    }


    public async Task<List<LeadPushProviderLog>> GetAllAsync()
    {
        return await _context.LeadPushProviderLogs
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
}
