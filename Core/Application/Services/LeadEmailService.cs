using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadEmailService
{
    private readonly AppDbContext _context;

    public LeadEmailService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadEmail> CreateAsync(
        LeadEmail email)
    {
        email.CreatedAt = DateTime.UtcNow;
        email.Status = "Draft";

        _context.LeadEmails.Add(email);

        await _context.SaveChangesAsync();

        return email;
    }


    public async Task<List<LeadEmail>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadEmails
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }



    public async Task<List<LeadEmail>> GetAllAsync()
    {
        return await _context.LeadEmails
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<LeadEmail?> GetByIdAsync(
        int id)
    {
        return await _context.LeadEmails
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> SendAsync(
        int id)
    {
        var email = await _context.LeadEmails
            .FirstOrDefaultAsync(x => x.Id == id);

        if (email == null)
            return false;


        email.Status = "Sent";
        email.SentAt = DateTime.UtcNow;
        email.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> UpdateAsync(
        LeadEmail updated)
    {
        var email = await _context.LeadEmails
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (email == null)
            return false;


        email.Subject = updated.Subject;
        email.Body = updated.Body;
        email.RecipientEmail = updated.RecipientEmail;
        email.Status = updated.Status;
        email.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        var email = await _context.LeadEmails
            .FirstOrDefaultAsync(x => x.Id == id);

        if (email == null)
            return false;


        _context.LeadEmails.Remove(email);

        await _context.SaveChangesAsync();

        return true;
    }
}
