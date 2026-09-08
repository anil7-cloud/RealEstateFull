namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionRestoreVerification
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionRestoreId { get; set; }

    public Guid? VerifiedByUserId { get; set; }

    public bool IsVerified { get; set; }

    public string? VerificationNotes { get; set; }

    public DateTime VerifiedAt { get; set; } = DateTime.UtcNow;

    public bool RequiresFollowUp { get; set; }

    public WorkflowExecutionRevisionRestore? WorkflowExecutionRevisionRestore { get; set; }
}
