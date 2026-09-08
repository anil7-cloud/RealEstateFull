using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadApiTokenService
{
    private readonly AppDbContext _context;

    public LeadApiTokenService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadApiToken> CreateAsync(LeadApiToken token)
    {
        token.CreatedAt = DateTime.UtcNow;
        token.IssuedAt = DateTime.UtcNow;
        token.IsActive = true;

        _context.LeadApiTokens.Add(token);

        await _context.SaveChangesAsync();

        return token;
    }

    public async Task<List<LeadApiToken>> GetAllAsync()
    {
        return await _context.LeadApiTokens
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<LeadApiToken?> GetByIdAsync(int id)
    {
        return await _context.LeadApiTokens
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<LeadApiToken?> GetByAccessTokenAsync(string accessToken)
    {
        return await _context.LeadApiTokens
            .FirstOrDefaultAsync(x =>
                x.AccessToken == accessToken &&
                x.IsActive &&
                !x.IsRevoked);
    }

    public async Task<List<LeadApiToken>> GetActiveAsync()
    {
        return await _context.LeadApiTokens
            .Where(x => x.IsActive && !x.IsRevoked)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> RegisterUsageAsync(int id, string ipAddress)
    {
        var token = await _context.LeadApiTokens
            .FirstOrDefaultAsync(x => x.Id == id);

        if (token == null)
            return false;

        token.LastUsedAt = DateTime.UtcNow;
        token.IpAddress = ipAddress;
        token.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RevokeAsync(int id, string reason)
    {
        var token = await _context.LeadApiTokens
            .FirstOrDefaultAsync(x => x.Id == id);

        if (token == null)
            return false;

        token.IsRevoked = true;
        token.IsActive = false;
        token.RevokedAt = DateTime.UtcNow;
        token.RevocationReason = reason;
        token.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var token = await _context.LeadApiTokens
            .FirstOrDefaultAsync(x => x.Id == id);

        if (token == null)
            return false;

        _context.LeadApiTokens.Remove(token);

        await _context.SaveChangesAsync();

        return true;
    }
}
