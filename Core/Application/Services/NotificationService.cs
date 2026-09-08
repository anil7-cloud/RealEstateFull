using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class NotificationService
{
    private readonly AppDbContext _context;

    public NotificationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddNotification(int userId, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return;

        var notification =
            new REAL_ESTATE_CLEAN.Core.Persistence.Notification
            {
                UserId = userId,
                Message = message.Trim(),
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
    }

    public async Task<
        List<REAL_ESTATE_CLEAN.Core.Persistence.Notification>>
        GetNotifications(int userId)
    {
        return await _context.Notifications
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<int> GetUnreadCount(int userId)
    {
        return await _context.Notifications
            .CountAsync(x =>
                x.UserId == userId &&
                !x.IsRead);
    }

    public async Task MarkAllAsRead(int userId)
    {
        var notifications = await _context.Notifications
            .Where(x =>
                x.UserId == userId &&
                !x.IsRead)
            .ToListAsync();

        if (notifications.Count == 0)
            return;

        foreach (var notification in notifications)
        {
            notification.IsRead = true;
        }

        await _context.SaveChangesAsync();
    }

    public async Task MarkAsRead(int notificationId)
    {
        REAL_ESTATE_CLEAN.Core.Persistence.Notification? notification =
            await _context.Notifications.FindAsync(notificationId);

        if (notification is null)
            return;

        notification.IsRead = true;

        await _context.SaveChangesAsync();
    }
}
