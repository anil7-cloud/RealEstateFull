namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyWorkflowAssignment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyWorkflowId { get; set; }

    public Guid AssignedUserId { get; set; }

    public string AssignmentRole { get; set; } = "Owner";

    public bool IsActive { get; set; } = true;

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RemovedAt { get; set; }

    public PropertyWorkflow? PropertyWorkflow { get; set; }
}
