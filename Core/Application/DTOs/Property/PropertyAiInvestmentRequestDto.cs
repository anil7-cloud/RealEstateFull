namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiInvestmentRequestDto
{
    public int PropertyId { get; set; }

    public decimal InvestmentAmount { get; set; }

    public decimal ExpectedReturn { get; set; }

    public decimal RiskScore { get; set; }

    public string InvestmentType { get; set; } = string.Empty;

    public string Analysis { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
