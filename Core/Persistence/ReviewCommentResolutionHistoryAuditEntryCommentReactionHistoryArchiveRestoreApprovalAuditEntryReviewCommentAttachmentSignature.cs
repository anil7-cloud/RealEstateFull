namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignature
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AttachmentId { get; set; }

    public string SignatureAlgorithm { get; set; } = string.Empty;

    public string SignatureValue { get; set; } = string.Empty;

    public string CertificateThumbprint { get; set; } = string.Empty;

    public Guid SignedByUserId { get; set; }

    public DateTime SignedAt { get; set; } = DateTime.UtcNow;

    public bool IsValid { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachment? Attachment { get; set; }
}
