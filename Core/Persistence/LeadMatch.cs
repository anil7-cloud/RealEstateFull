namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadMatch
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int PropertyId { get; set; }

    public decimal MatchScore { get; set; }

    public string MatchLevel { get; set; } = string.Empty;

    public bool IsRecommended { get; set; }

    public bool IsViewed { get; set; }

    public bool IsContacted { get; set; }

    public DateTime MatchedAt { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
