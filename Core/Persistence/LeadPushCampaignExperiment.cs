namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignExperiment
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string VariantKey { get; set; } = string.Empty;

    public int AllocationPercentage { get; set; }

    public bool IsControlGroup { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
