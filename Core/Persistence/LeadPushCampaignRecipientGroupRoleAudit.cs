namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleAudit
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string? PerformedBy { get; set; }

    public string? PreviousValue { get; set; }

    public string? NewValue { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRole? GroupRole { get; set; }
}
