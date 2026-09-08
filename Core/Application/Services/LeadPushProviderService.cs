using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushProviderService
{
    private readonly AppDbContext _context;

    public LeadPushProviderService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadPushProvider> CreateAsync(
        string name,
        string providerType)
    {
        var provider = new LeadPushProvider
        {
            Name = name,
            ProviderType = providerType,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadPushProviders.Add(provider);

        await _context.SaveChangesAsync();

        return provider;
    }


    public async Task<List<LeadPushProvider>> GetActiveAsync()
    {
        return await _context.LeadPushProviders
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }


    public async Task<bool> DisableAsync(int id)
    {
        var provider = await _context.LeadPushProviders
            .FirstOrDefaultAsync(x => x.Id == id);

        if (provider == null)
            return false;

        provider.IsActive = false;
        provider.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
