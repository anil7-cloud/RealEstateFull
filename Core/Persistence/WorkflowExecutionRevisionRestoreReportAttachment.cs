namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionRestoreReportAttachment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionRestoreReportId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public string? ContentType { get; set; }

    public long FileSize { get; set; }

    public Guid? UploadedByUserId { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionRestoreReport? WorkflowExecutionRevisionRestoreReport { get; set; }
}
