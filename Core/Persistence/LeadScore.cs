namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadScore
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    // Toplam puan (0-100)
    public int Score { get; set; }

    public int BudgetScore { get; set; }

    public int InterestScore { get; set; }

    public int CommunicationScore { get; set; }

    public int EngagementScore { get; set; }

    public int UrgencyScore { get; set; }

    // Cold, Warm, Hot
    public string Category { get; set; } = "Cold";

    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
