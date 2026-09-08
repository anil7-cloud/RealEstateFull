namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleAssignment
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleId { get; set; }

    public int LeadPushCampaignRecipientId { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RevokedAt { get; set; }

    public string? AssignedBy { get; set; }

    public LeadPushCampaignRecipientGroupRole? GroupRole { get; set; }

    public LeadPushCampaignRecipient? Recipient { get; set; }
}
