namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryComment
{
    public int Id { get; set; }

    public int ReviewCommentResolutionHistoryAuditEntryId { get; set; }

    public string Comment { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsInternal { get; set; }

    public ReviewCommentResolutionHistoryAuditEntry? AuditEntry { get; set; }
}
