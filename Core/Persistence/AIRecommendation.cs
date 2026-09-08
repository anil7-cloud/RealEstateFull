namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class AIRecommendation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CustomerUserId { get; set; }

    public Guid? PropertyListingId { get; set; }

    public string RecommendationType { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public decimal MatchScore { get; set; }

    public bool IsAccepted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyListing? PropertyListing { get; set; }
}
