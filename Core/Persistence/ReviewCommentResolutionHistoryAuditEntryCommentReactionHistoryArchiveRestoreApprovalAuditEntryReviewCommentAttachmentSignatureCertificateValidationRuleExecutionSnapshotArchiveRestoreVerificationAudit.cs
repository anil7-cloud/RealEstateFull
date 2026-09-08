namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAudit
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationId { get; set; }

    public Guid AuditedByUserId { get; set; }

    public DateTime AuditedAt { get; set; } = DateTime.UtcNow;

    public bool Passed { get; set; }

    public string? Findings { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerification? Verification { get; set; }
}
