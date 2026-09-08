namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRole
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupId { get; set; }

    public string RoleName { get; set; } = string.Empty;

    public string RoleCode { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsDefault { get; set; }

    public bool IsSystemRole { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroup? Group { get; set; }
}
