namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationLog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CertificateId { get; set; }

    public Guid ValidatedByUserId { get; set; }

    public bool IsValid { get; set; }

    public string ValidationMethod { get; set; } = string.Empty;

    public string? ValidationMessage { get; set; }

    public DateTime ValidatedAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificate? Certificate { get; set; }
}
