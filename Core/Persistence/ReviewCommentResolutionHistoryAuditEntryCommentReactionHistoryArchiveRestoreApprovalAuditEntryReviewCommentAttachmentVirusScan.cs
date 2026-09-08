namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentVirusScan
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AttachmentId { get; set; }

    public string EngineName { get; set; } = string.Empty;

    public string EngineVersion { get; set; } = string.Empty;

    public bool IsInfected { get; set; }

    public string? ThreatName { get; set; }

    public DateTime ScannedAt { get; set; } = DateTime.UtcNow;

    public Guid ScannedByUserId { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachment? Attachment { get; set; }
}
