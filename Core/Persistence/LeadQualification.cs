namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadQualification
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    public int BudgetScore { get; set; }

    public int InterestScore { get; set; }

    public int UrgencyScore { get; set; }

    public int AuthorityScore { get; set; }

    public int NeedScore { get; set; }

    public int TimelineScore { get; set; }

    public int TotalScore { get; set; }

    public string QualificationLevel { get; set; } = string.Empty;

    public bool IsQualified { get; set; }

    public string Notes { get; set; } = string.Empty;

    public DateTime QualifiedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
