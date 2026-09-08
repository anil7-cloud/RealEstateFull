namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientSegment
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientId { get; set; }

    public string SegmentName { get; set; } = string.Empty;

    public string? SegmentCode { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RemovedAt { get; set; }

    public string? Notes { get; set; }

    public LeadPushCampaignRecipient? Recipient { get; set; }
}
