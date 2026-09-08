namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureVerification
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SignatureId { get; set; }

    public Guid AttachmentId { get; set; }

    public Guid VerifiedByUserId { get; set; }

    public bool IsSignatureValid { get; set; }

    public string? VerificationDetails { get; set; }

    public DateTime VerifiedAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignature? Signature { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachment? Attachment { get; set; }
}
