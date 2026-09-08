namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyWorkflowAction
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyWorkflowId { get; set; }

    public string ActionName { get; set; } = string.Empty;

    public string ActionType { get; set; } = string.Empty;

    public string? ActionDataJson { get; set; }

    public Guid? ExecutedByUserId { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime? ExecutedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyWorkflow? PropertyWorkflow { get; set; }
}
