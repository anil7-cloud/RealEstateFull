namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyWorkflowHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyWorkflowId { get; set; }

    public Guid? UserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string? PreviousStatus { get; set; }

    public string? NewStatus { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyWorkflow? PropertyWorkflow { get; set; }
}
