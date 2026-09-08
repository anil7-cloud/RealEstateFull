namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignExecution
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public string Status { get; set; } = "Pending";

    public int TotalRecipients { get; set; }

    public int SuccessCount { get; set; }

    public int FailedCount { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
