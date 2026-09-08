namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAudit
{
    public int Id { get; set; }

    public int ReviewCommentResolutionHistoryId { get; set; }

    public string AuditAction { get; set; } = string.Empty;

    public string PerformedBy { get; set; } = string.Empty;

    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;

    public string? Notes { get; set; }

    public string? IpAddress { get; set; }

    public ReviewCommentResolutionHistory? ReviewCommentResolutionHistory { get; set; }
}
