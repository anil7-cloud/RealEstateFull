namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalAudit
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalId { get; set; }

    public string AuditType { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public string? PerformedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApproval? Approval { get; set; }
}
