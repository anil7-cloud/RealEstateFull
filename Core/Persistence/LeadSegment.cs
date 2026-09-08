namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadSegment
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string SegmentName { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public decimal? MinBudget { get; set; }

    public decimal? MaxBudget { get; set; }

    public string Region { get; set; } = string.Empty;

    public int Score { get; set; }

    public int Priority { get; set; }

    public bool IsVip { get; set; }

    public bool IsQualified { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
