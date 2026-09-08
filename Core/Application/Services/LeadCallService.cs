using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadCallService
{
    private readonly AppDbContext _context;

    public LeadCallService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<LeadCall>> GetAllAsync()
    {
        return await _context.LeadCalls
            .OrderByDescending(x => x.CallDate)
            .ToListAsync();
    }


    public async Task<List<LeadCall>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadCalls
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CallDate)
            .ToListAsync();
    }


    public async Task<LeadCall> CreateAsync(LeadCall call)
    {
        call.CreatedAt = DateTime.UtcNow;
        call.CallDate = DateTime.UtcNow;
        call.Status = "Scheduled";

        _context.LeadCalls.Add(call);

        await _context.SaveChangesAsync();

        return call;
    }


    public async Task<bool> UpdateStatusAsync(
        int id,
        string status,
        string result)
    {
        var call = await _context.LeadCalls
            .FirstOrDefaultAsync(x => x.Id == id);

        if (call == null)
            return false;


        call.Status = status;
        call.Result = result;
        call.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var call = await _context.LeadCalls
            .FirstOrDefaultAsync(x => x.Id == id);

        if (call == null)
            return false;


        _context.LeadCalls.Remove(call);

        await _context.SaveChangesAsync();

        return true;
    }
}
