namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadOpportunity
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? PropertyId { get; set; }

    public string OpportunityName { get; set; } = string.Empty;

    public string Stage { get; set; } = string.Empty;

    public decimal EstimatedValue { get; set; }

    public decimal Probability { get; set; }

    public DateTime? ExpectedCloseDate { get; set; }

    public string Source { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsWon { get; set; }

    public bool IsLost { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
