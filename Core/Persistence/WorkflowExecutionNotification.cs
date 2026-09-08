namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionNotification
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionHistoryId { get; set; }

    public Guid RecipientUserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ReadAt { get; set; }

    public WorkflowExecutionHistory? WorkflowExecutionHistory { get; set; }
}
