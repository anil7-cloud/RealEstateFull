namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalNotification
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalId { get; set; }

    public string Channel { get; set; } = string.Empty;

    public string Recipient { get; set; } = string.Empty;

    public string NotificationType { get; set; } = string.Empty;

    public bool IsSent { get; set; }

    public DateTime? SentAt { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApproval? Approval { get; set; }
}
