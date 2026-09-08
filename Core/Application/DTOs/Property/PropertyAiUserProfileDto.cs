namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiUserProfileDto
{
    public int UserId { get; set; }

    public string UserType { get; set; } = string.Empty;

    public string Preferences { get; set; } = string.Empty;

    public string BehaviorSummary { get; set; } = string.Empty;

    public decimal MatchScore { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
