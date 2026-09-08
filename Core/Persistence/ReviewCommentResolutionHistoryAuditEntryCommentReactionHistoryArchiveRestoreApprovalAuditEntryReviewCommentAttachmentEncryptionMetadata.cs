namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentEncryptionMetadata
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AttachmentId { get; set; }

    public string Algorithm { get; set; } = string.Empty;

    public string KeyIdentifier { get; set; } = string.Empty;

    public string? InitializationVector { get; set; }

    public bool IsEncrypted { get; set; }

    public DateTime EncryptedAt { get; set; } = DateTime.UtcNow;

    public Guid EncryptedByUserId { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachment? Attachment { get; set; }
}
