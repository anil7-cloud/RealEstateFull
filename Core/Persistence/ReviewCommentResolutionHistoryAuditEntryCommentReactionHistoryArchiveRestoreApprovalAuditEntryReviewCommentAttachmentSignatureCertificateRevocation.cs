namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateRevocation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CertificateId { get; set; }

    public DateTime RevokedAt { get; set; }

    public string RevocationReason { get; set; } = string.Empty;

    public string? RevokedByAuthority { get; set; }

    public string? CrlDistributionPoint { get; set; }

    public bool IsActive { get; set; }

    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificate? Certificate { get; set; }
}
