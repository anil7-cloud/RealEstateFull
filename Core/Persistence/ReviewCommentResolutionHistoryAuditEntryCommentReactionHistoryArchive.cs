namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchive
{
    public int Id { get; set; }

    public int ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryId { get; set; }

    public DateTime ArchivedAt { get; set; } = DateTime.UtcNow;

    public string ArchivedBy { get; set; } = string.Empty;

    public string ArchiveReason { get; set; } = string.Empty;

    public bool IsCompressed { get; set; }

    public string? StorageLocation { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistory? ReactionHistory { get; set; }
}
