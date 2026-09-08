namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushSegment
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string FilterExpression { get; set; } = string.Empty;

    public int UserCount { get; set; }

    public bool IsDynamic { get; set; } = true;

    public bool IsActive { get; set; } = true;

    public DateTime LastCalculatedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
