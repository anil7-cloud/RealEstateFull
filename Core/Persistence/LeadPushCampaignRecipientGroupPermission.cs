namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupPermission
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupId { get; set; }

    public string PermissionName { get; set; } = string.Empty;

    public string PermissionCode { get; set; } = string.Empty;

    public bool IsGranted { get; set; } = true;

    public string? GrantedBy { get; set; }

    public DateTime GrantedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RevokedAt { get; set; }

    public LeadPushCampaignRecipientGroup? Group { get; set; }
}
