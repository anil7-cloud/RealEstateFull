using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushAudienceService
{
    private readonly AppDbContext _context;

    public LeadPushAudienceService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadPushAudience> CreateAsync(
        string name,
        string description)
    {
        var audience = new LeadPushAudience
        {
            Name = name,
            Description = description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadPushAudiences.Add(audience);

        await _context.SaveChangesAsync();

        return audience;
    }


    public async Task<List<LeadPushAudience>> GetActiveAsync()
    {
        return await _context.LeadPushAudiences
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }


    public async Task<bool> DisableAsync(int id)
    {
        var audience = await _context.LeadPushAudiences
            .FirstOrDefaultAsync(x => x.Id == id);

        if (audience == null)
            return false;

        audience.IsActive = false;
        audience.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
