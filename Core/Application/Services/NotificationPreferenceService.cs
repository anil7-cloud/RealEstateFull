using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class NotificationPreferenceService
{
    private readonly AppDbContext _context;

    public NotificationPreferenceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<NotificationPreference> Create(
        int userId,
        bool email,
        bool sms,
        bool push)
    {
        var preference = new NotificationPreference
        {
            UserId = userId,
            EmailEnabled = email,
            SmsEnabled = sms,
            PushEnabled = push
        };

        _context.NotificationPreferences.Add(preference);
        await _context.SaveChangesAsync();

        return preference;
    }
}
