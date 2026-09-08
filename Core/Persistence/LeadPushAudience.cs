namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushAudience
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string FilterJson { get; set; } = string.Empty;

    public int EstimatedCount { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public int TotalUsers { get; set; }
}
