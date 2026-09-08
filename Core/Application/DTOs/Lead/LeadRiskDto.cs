namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadRiskDto
{
    public int LeadId { get; set; }

    public string RiskLevel { get; set; } = "Low";

    public string RiskReason { get; set; } = string.Empty;

    public decimal RiskScore { get; set; }

    public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;
}
