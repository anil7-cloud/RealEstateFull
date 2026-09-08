using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadApiKeyService
{
    private readonly AppDbContext _context;

    public LeadApiKeyService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadApiKey> CreateAsync(LeadApiKey apiKey)
    {
        apiKey.CreatedAt = DateTime.UtcNow;
        apiKey.IsActive = true;

        _context.LeadApiKeys.Add(apiKey);

        await _context.SaveChangesAsync();

        return apiKey;
    }

    public async Task<List<LeadApiKey>> GetAllAsync()
    {
        return await _context.LeadApiKeys
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<LeadApiKey?> GetByIdAsync(int id)
    {
        return await _context.LeadApiKeys
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<LeadApiKey?> GetByApiKeyAsync(string apiKey)
    {
        return await _context.LeadApiKeys
            .FirstOrDefaultAsync(x =>
                x.ApiKey == apiKey &&
                x.IsActive &&
                !x.IsRevoked);
    }

    public async Task<List<LeadApiKey>> GetActiveAsync()
    {
        return await _context.LeadApiKeys
            .Where(x => x.IsActive && !x.IsRevoked)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<bool> RegisterUsageAsync(int id, string ipAddress)
    {
        var apiKey = await _context.LeadApiKeys
            .FirstOrDefaultAsync(x => x.Id == id);

        if (apiKey == null)
            return false;

        apiKey.RequestCount++;
        apiKey.LastUsedAt = DateTime.UtcNow;
        apiKey.LastIpAddress = ipAddress;
        apiKey.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(LeadApiKey updated)
    {
        var apiKey = await _context.LeadApiKeys
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (apiKey == null)
            return false;

        apiKey.Name = updated.Name;
        apiKey.Description = updated.Description;
        apiKey.SecretKey = updated.SecretKey;
        apiKey.PermissionsJson = updated.PermissionsJson;
        apiKey.IsActive = updated.IsActive;
        apiKey.DailyLimit = updated.DailyLimit;
        apiKey.MonthlyLimit = updated.MonthlyLimit;
        apiKey.ExpiresAt = updated.ExpiresAt;
        apiKey.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RevokeAsync(int id)
    {
        var apiKey = await _context.LeadApiKeys
            .FirstOrDefaultAsync(x => x.Id == id);

        if (apiKey == null)
            return false;

        apiKey.IsRevoked = true;
        apiKey.IsActive = false;
        apiKey.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var apiKey = await _context.LeadApiKeys
            .FirstOrDefaultAsync(x => x.Id == id);

        if (apiKey == null)
            return false;

        _context.LeadApiKeys.Remove(apiKey);

        await _context.SaveChangesAsync();

        return true;
    }
}
