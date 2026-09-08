namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionAttachment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionStepId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public string? ContentType { get; set; }

    public long FileSize { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public Guid UploadedByUserId { get; set; }

    public WorkflowExecutionStep? WorkflowExecutionStep { get; set; }
}
