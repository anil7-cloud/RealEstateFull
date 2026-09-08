namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeAudit
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeId { get; set; }

    public string AuditType { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public string? PerformedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChange? RoleChange { get; set; }
}
