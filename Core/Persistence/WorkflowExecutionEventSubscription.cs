namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionEventSubscription
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionEventId { get; set; }

    public Guid SubscriberUserId { get; set; }

    public string SubscriptionType { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ExpiresAt { get; set; }

    public string? FilterExpression { get; set; }

    public WorkflowExecutionEvent? WorkflowExecutionEvent { get; set; }
}
