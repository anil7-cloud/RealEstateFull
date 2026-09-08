namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionRestore
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionArchiveId { get; set; }

    public Guid? RestoredByUserId { get; set; }

    public string RestoreReason { get; set; } = string.Empty;

    public bool IsSuccessful { get; set; }

    public string? ResultMessage { get; set; }

    public DateTime RestoredAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionArchive? WorkflowExecutionRevisionArchive { get; set; }
}
