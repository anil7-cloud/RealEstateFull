namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionApproval
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionId { get; set; }

    public Guid ApproverUserId { get; set; }

    public string Status { get; set; } = "Pending";

    public string? Comment { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevision? WorkflowExecutionRevision { get; set; }
}
