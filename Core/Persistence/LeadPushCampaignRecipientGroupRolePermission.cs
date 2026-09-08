namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRolePermission
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleId { get; set; }

    public string PermissionName { get; set; } = string.Empty;

    public string PermissionCode { get; set; } = string.Empty;

    public bool IsAllowed { get; set; } = true;

    public string? Description { get; set; }

    public DateTime GrantedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRole? GroupRole { get; set; }
}
