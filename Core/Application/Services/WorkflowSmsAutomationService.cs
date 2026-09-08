using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowSmsAutomationService
{
    private readonly AppDbContext _context;

    public WorkflowSmsAutomationService(AppDbContext context)
    {
        _context = context;
    }


    public async Task QueueSmsAsync(
        int leadId,
        string phone,
        string message)
    {
        var queue = new LeadNotificationQueue
        {
            LeadId = leadId,
            Recipient = phone,
            Channel = "SMS",
            Title = "Workflow SMS",
            Message = message,
            Status = "Pending",
            RetryCount = 0,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadNotificationQueues.Add(queue);

        await _context.SaveChangesAsync();
    }


    public async Task<List<LeadNotificationQueue>> GetPendingSmsAsync()
    {
        return await _context.LeadNotificationQueues
            .Where(x =>
                x.Channel == "SMS" &&
                x.Status == "Pending")
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task MarkSmsSentAsync(int id)
    {
        var sms = await _context.LeadNotificationQueues
            .FirstOrDefaultAsync(x => x.Id == id);

        if (sms == null)
            return;

        sms.Status = "Sent";
        sms.IsCompleted = true;
        sms.SentAt = DateTime.UtcNow;
        sms.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
}
