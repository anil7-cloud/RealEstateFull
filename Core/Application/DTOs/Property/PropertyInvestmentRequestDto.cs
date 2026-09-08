namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyInvestmentRequestDto
{
    public int PropertyId { get; set; }

    public decimal InvestmentAmount { get; set; }

    public string InvestmentType { get; set; } = string.Empty;

    public decimal ExpectedReturn { get; set; }

    public DateTime InvestmentDate { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;
}
