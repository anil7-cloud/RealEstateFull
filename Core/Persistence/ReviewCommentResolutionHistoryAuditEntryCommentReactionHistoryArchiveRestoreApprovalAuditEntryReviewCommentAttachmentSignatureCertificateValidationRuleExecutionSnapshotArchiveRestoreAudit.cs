namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreAudit
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SnapshotArchiveRestoreId { get; set; }

    public Guid PerformedByUserId { get; set; }

    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;

    public string Action { get; set; } = string.Empty;

    public bool IsSuccessful { get; set; }

    public string? Details { get; set; }

    public string? IpAddress { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestore? SnapshotArchiveRestore { get; set; }
}
