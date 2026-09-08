namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CertificateId { get; set; }

    public string PolicyName { get; set; } = string.Empty;

    public string PolicyVersion { get; set; } = string.Empty;

    public bool CheckExpiration { get; set; }

    public bool CheckRevocation { get; set; }

    public bool CheckIssuerTrust { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificate? Certificate { get; set; }
}
