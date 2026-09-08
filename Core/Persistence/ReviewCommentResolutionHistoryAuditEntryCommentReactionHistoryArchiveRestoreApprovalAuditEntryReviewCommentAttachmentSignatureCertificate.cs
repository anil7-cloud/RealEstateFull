namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificate
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SignatureId { get; set; }

    public string Subject { get; set; } = string.Empty;

    public string Issuer { get; set; } = string.Empty;

    public string SerialNumber { get; set; } = string.Empty;

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    public string Thumbprint { get; set; } = string.Empty;

    public bool IsRevoked { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignature? Signature { get; set; }
}
