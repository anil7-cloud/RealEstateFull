using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadService
{
    private readonly AppDbContext _context;

    public LeadService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<Lead>> GetAllAsync()
    {
        return await _context.Leads.ToListAsync();
    }


    public async Task<Lead?> GetByIdAsync(int id)
    {
        return await _context.Leads
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<Lead> CreateAsync(Lead lead)
    {
        lead.CreatedAt = DateTime.UtcNow;

        _context.Leads.Add(lead);

        await _context.SaveChangesAsync();

        return lead;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var lead = await _context.Leads
            .FirstOrDefaultAsync(x => x.Id == id);

        if (lead == null)
            return false;

        _context.Leads.Remove(lead);

        await _context.SaveChangesAsync();

        return true;
    }
}
