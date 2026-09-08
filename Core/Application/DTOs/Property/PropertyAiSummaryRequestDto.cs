namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiSummaryRequestDto
{
    public int PropertyId { get; set; }

    public bool IncludePriceAnalysis { get; set; } = true;

    public bool IncludeMarketAnalysis { get; set; } = true;

    public bool IncludeFeatureAnalysis { get; set; } = true;

    public string Language { get; set; } = "tr";
}
