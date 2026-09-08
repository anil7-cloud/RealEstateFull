namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignApproval
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public int? RequestedByUserId { get; set; }

    public int? ApprovedByUserId { get; set; }

    public string Status { get; set; } = "Pending";

    public string Comments { get; set; } = string.Empty;

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ReviewedAt { get; set; }

    public LeadPushCampaign? Campaign { get; set; }
}
