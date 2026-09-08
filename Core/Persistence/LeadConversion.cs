namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadConversion
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? PropertyId { get; set; }

    public int? UserId { get; set; }

    // New, Qualified, Proposal, Negotiation, Won, Lost
    public string FromStage { get; set; } = string.Empty;

    public string ToStage { get; set; } = string.Empty;

    public decimal ExpectedValue { get; set; }

    public decimal FinalValue { get; set; }

    public bool IsWon { get; set; }

    public string LostReason { get; set; } = string.Empty;

    public DateTime ConversionDate { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
