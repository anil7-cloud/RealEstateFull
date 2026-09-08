using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowNotificationAutomationService
{
    private readonly AppDbContext _context;

    public WorkflowNotificationAutomationService(AppDbContext context)
    {
        _context = context;
    }


    public async Task CreateNotificationAsync(
        int leadId,
        string title,
        string message)
    {
        var notification = new LeadNotification
        {
            LeadId = leadId,
            Title = title,
            Message = message,
            Type = "Workflow",
            Priority = "Normal",
            IsRead = false,
            IsSent = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadNotifications.Add(notification);

        await _context.SaveChangesAsync();
    }


    public async Task<List<LeadNotification>> GetWorkflowNotificationsAsync(
        int leadId)
    {
        return await _context.LeadNotifications
            .Where(x =>
                x.LeadId == leadId &&
                x.Type == "Workflow")
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
}
