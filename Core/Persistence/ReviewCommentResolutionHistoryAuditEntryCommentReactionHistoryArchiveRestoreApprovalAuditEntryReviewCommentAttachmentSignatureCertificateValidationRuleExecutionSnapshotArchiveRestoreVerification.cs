namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerification
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SnapshotArchiveRestoreId { get; set; }

    public Guid VerifiedByUserId { get; set; }

    public DateTime VerifiedAt { get; set; } = DateTime.UtcNow;

    public bool IsVerified { get; set; }

    public string? VerificationDetails { get; set; }

    public string? ExpectedChecksum { get; set; }

    public string? ActualChecksum { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestore? SnapshotArchiveRestore { get; set; }
}
