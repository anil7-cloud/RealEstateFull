namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadLostReason
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    public string ReasonCategory { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public string CompetitorName { get; set; } = string.Empty;

    public decimal? CompetitorPrice { get; set; }

    public bool CanBeRecovered { get; set; }

    public DateTime? RecoveryDate { get; set; }

    public string RecoveryPlan { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime LostDate { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
