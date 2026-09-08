namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiSummaryResultDto
{
    public int PropertyId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public decimal MarketValue { get; set; }

    public decimal Score { get; set; }

    public List<string> KeyPoints { get; set; } = new();
}
