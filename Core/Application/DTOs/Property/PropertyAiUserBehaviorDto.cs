namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiUserBehaviorDto
{
    public int UserId { get; set; }

    public int ViewCount { get; set; }

    public int FavoriteCount { get; set; }

    public int SearchCount { get; set; }

    public int OfferCount { get; set; }

    public string BehaviorSummary { get; set; } = string.Empty;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
