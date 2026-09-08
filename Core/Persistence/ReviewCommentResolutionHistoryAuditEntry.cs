namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntry
{
    public int Id { get; set; }

    public int ReviewCommentResolutionHistoryAuditId { get; set; }

    public string PropertyName { get; set; } = string.Empty;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public string ChangeType { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolutionHistoryAudit? Audit { get; set; }
}
