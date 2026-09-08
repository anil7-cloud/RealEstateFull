namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignExecutionLog
{
    public int Id { get; set; }

    public int LeadPushCampaignExecutionId { get; set; }

    public string Level { get; set; } = "Information";

    public string Message { get; set; } = string.Empty;

    public string? Exception { get; set; }

    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignExecution? Execution { get; set; }
}
