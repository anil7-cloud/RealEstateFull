namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadRiskAssessment
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    public string RiskCategory { get; set; } = string.Empty;

    public string RiskLevel { get; set; } = string.Empty;

    public decimal RiskScore { get; set; }

    public decimal Probability { get; set; }

    public decimal Impact { get; set; }

    public string Description { get; set; } = string.Empty;

    public string MitigationPlan { get; set; } = string.Empty;

    public bool IsResolved { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
