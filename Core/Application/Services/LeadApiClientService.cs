using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadApiClientService
{
    private readonly AppDbContext _context;

    public LeadApiClientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadApiClient> CreateAsync(LeadApiClient client)
    {
        client.CreatedAt = DateTime.UtcNow;
        client.IsActive = true;

        _context.LeadApiClients.Add(client);

        await _context.SaveChangesAsync();

        return client;
    }

    public async Task<List<LeadApiClient>> GetAllAsync()
    {
        return await _context.LeadApiClients
            .OrderBy(x => x.ClientName)
            .ToListAsync();
    }

    public async Task<LeadApiClient?> GetByIdAsync(int id)
    {
        return await _context.LeadApiClients
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadApiClient>> GetActiveAsync()
    {
        return await _context.LeadApiClients
            .Where(x => x.IsActive && !x.IsBlocked)
            .OrderBy(x => x.ClientName)
            .ToListAsync();
    }

    public async Task<bool> RegisterRequestAsync(int id)
    {
        var client = await _context.LeadApiClients
            .FirstOrDefaultAsync(x => x.Id == id);

        if (client == null)
            return false;

        client.TotalRequests++;
        client.LastRequestAt = DateTime.UtcNow;
        client.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(LeadApiClient updated)
    {
        var client = await _context.LeadApiClients
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (client == null)
            return false;

        client.ClientName = updated.ClientName;
        client.ClientCode = updated.ClientCode;
        client.ContactPerson = updated.ContactPerson;
        client.Email = updated.Email;
        client.Phone = updated.Phone;
        client.Company = updated.Company;
        client.Website = updated.Website;
        client.BaseUrl = updated.BaseUrl;
        client.AllowedIpAddresses = updated.AllowedIpAddresses;
        client.AllowedOrigins = updated.AllowedOrigins;
        client.Notes = updated.Notes;
        client.DailyRequestLimit = updated.DailyRequestLimit;
        client.MonthlyRequestLimit = updated.MonthlyRequestLimit;
        client.IsActive = updated.IsActive;
        client.IsBlocked = updated.IsBlocked;
        client.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> BlockAsync(int id)
    {
        var client = await _context.LeadApiClients
            .FirstOrDefaultAsync(x => x.Id == id);

        if (client == null)
            return false;

        client.IsBlocked = true;
        client.IsActive = false;
        client.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var client = await _context.LeadApiClients
            .FirstOrDefaultAsync(x => x.Id == id);

        if (client == null)
            return false;

        _context.LeadApiClients.Remove(client);

        await _context.SaveChangesAsync();

        return true;
    }
}
