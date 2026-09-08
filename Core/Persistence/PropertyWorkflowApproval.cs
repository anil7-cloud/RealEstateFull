namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyWorkflowApproval
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyWorkflowId { get; set; }

    public Guid ApproverUserId { get; set; }

    public string ApprovalType { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public string? Comment { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyWorkflow? PropertyWorkflow { get; set; }
}
