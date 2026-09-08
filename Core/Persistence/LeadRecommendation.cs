namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadRecommendation
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int PropertyId { get; set; }

    public decimal RecommendationScore { get; set; }

    public string RecommendationReason { get; set; } = string.Empty;

    public bool IsSent { get; set; }

    public DateTime? SentAt { get; set; }

    public bool IsViewed { get; set; }

    public DateTime? ViewedAt { get; set; }

    public bool IsInterested { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
