namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadRiskResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string RiskLevel { get; set; } = string.Empty;

    public string RiskReason { get; set; } = string.Empty;

    public decimal RiskScore { get; set; }

    public DateTime AnalyzedAt { get; set; }
}
