using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowEmailAutomationService
{
    private readonly AppDbContext _context;

    public WorkflowEmailAutomationService(AppDbContext context)
    {
        _context = context;
    }


    public async Task QueueEmailAsync(
        int leadId,
        string email,
        string subject,
        string body)
    {
        var queue = new LeadNotificationQueue
        {
            LeadId = leadId,
            Recipient = email,
            Channel = "Email",
            Title = subject,
            Message = body,
            Status = "Pending",
            RetryCount = 0,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadNotificationQueues.Add(queue);

        await _context.SaveChangesAsync();
    }


    public async Task<List<LeadNotificationQueue>> GetPendingEmailsAsync()
    {
        return await _context.LeadNotificationQueues
            .Where(x =>
                x.Channel == "Email" &&
                x.Status == "Pending")
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task MarkEmailSentAsync(int id)
    {
        var item = await _context.LeadNotificationQueues
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return;

        item.Status = "Sent";
        item.IsCompleted = true;
        item.SentAt = DateTime.UtcNow;
        item.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
}
