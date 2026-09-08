using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushNotificationQueueService
{
    private readonly AppDbContext _context;
    private readonly FcmPushSenderService _fcm;

    public LeadPushNotificationQueueService(
        AppDbContext context,
        FcmPushSenderService fcm)
    {
        _context = context;
        _fcm = fcm;
    }

    public async Task<LeadPushNotificationQueue> CreateAsync(
        int leadId,
        string title,
        string body)
    {
        var queue = new LeadPushNotificationQueue
        {
            LeadId = leadId,
            Title = title?.Trim() ?? "",
            Body = body?.Trim() ?? "",
            Status = "Pending",
            Priority = "High",
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadPushNotificationQueues.Add(queue);

        await _context.SaveChangesAsync();

        return queue;
    }

    public async Task<List<LeadPushNotificationQueue>> GetPendingAsync()
    {
        return await _context.LeadPushNotificationQueues
            .Where(x => x.Status == "Pending")
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> CompleteAsync(int id)
    {
        var item =
            await _context.LeadPushNotificationQueues
                .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return false;

        item.Status = "Completed";
        item.IsCompleted = true;
        item.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ProcessAsync(
        int id,
        string deviceToken)
    {
        var item =
            await _context.LeadPushNotificationQueues
                .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return false;

        if (string.IsNullOrWhiteSpace(deviceToken))
        {
            item.Status = "Failed";
            item.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return false;
        }

        try
        {
            var messageId = await _fcm.SendAsync(
                deviceToken,
                item.Title,
                item.Body);

            item.Status = "Completed";
            item.IsCompleted = true;
            item.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            Console.WriteLine(
                $"FCM push gönderildi. QueueId={id}, MessageId={messageId}");

            return true;
        }
        catch (Exception ex)
        {
            item.Status = "Failed";
            item.IsCompleted = false;
            item.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            Console.WriteLine(
                $"FCM push başarısız. QueueId={id}, Error={ex.Message}");

            return false;
        }
    }
}
