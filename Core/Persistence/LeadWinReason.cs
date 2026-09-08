namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadWinReason
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    public string WinCategory { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public decimal SalePrice { get; set; }

    public decimal CommissionAmount { get; set; }

    public string CompetitiveAdvantage { get; set; } = string.Empty;

    public string CustomerDecisionFactor { get; set; } = string.Empty;

    public string SuccessStrategy { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime WonDate { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
