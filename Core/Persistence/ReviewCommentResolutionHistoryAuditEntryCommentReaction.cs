namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReaction
{
    public int Id { get; set; }

    public int ReviewCommentResolutionHistoryAuditEntryCommentId { get; set; }

    public string ReactionType { get; set; } = string.Empty;

    public string ReactedBy { get; set; } = string.Empty;

    public DateTime ReactedAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolutionHistoryAuditEntryComment? Comment { get; set; }
}
