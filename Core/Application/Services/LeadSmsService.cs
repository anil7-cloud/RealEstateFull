using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadSmsService
{
    private readonly AppDbContext _context;

    public LeadSmsService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<LeadSms>> GetAllAsync()
    {
        return await _context.LeadSms
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<LeadSms>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadSms
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<LeadSms> CreateAsync(LeadSms sms)
    {
        sms.CreatedAt = DateTime.UtcNow;
        sms.Status = "Draft";

        _context.LeadSms.Add(sms);

        await _context.SaveChangesAsync();

        return sms;
    }


    public async Task<bool> UpdateStatusAsync(int id, string status)
    {
        var sms = await _context.LeadSms
            .FirstOrDefaultAsync(x => x.Id == id);

        if (sms == null)
            return false;


        sms.Status = status;
        sms.UpdatedAt = DateTime.UtcNow;


        if (status == "Sent")
            sms.SentAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var sms = await _context.LeadSms
            .FirstOrDefaultAsync(x => x.Id == id);

        if (sms == null)
            return false;


        _context.LeadSms.Remove(sms);

        await _context.SaveChangesAsync();

        return true;
    }
}
