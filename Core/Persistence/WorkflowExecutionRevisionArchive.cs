namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionArchive
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionId { get; set; }

    public Guid? ArchivedByUserId { get; set; }

    public string ArchiveReason { get; set; } = string.Empty;

    public string ArchiveDataJson { get; set; } = "{}";

    public DateTime ArchivedAt { get; set; } = DateTime.UtcNow;

    public bool CanRestore { get; set; } = true;

    public WorkflowExecutionRevision? WorkflowExecutionRevision { get; set; }
}
