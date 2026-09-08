using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadAlertService
{
    private readonly AppDbContext _context;

    public LeadAlertService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadAlert> CreateAsync(
        int leadId,
        string alertName,
        string city,
        string propertyType)
    {
        var alert = new LeadAlert
        {
            LeadId = leadId,
            AlertName = alertName,
            City = city,
            PropertyType = propertyType,
            IsEnabled = true,
            EmailNotification = true,
            SmsNotification = false,
            PushNotification = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadAlerts.Add(alert);

        await _context.SaveChangesAsync();

        return alert;
    }


    public async Task<List<LeadAlert>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadAlerts
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> DisableAsync(
        int id)
    {
        var alert = await _context.LeadAlerts
            .FirstOrDefaultAsync(x => x.Id == id);

        if (alert == null)
            return false;

        alert.IsEnabled = false;
        alert.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
