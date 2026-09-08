namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiComparisonRequestDto
{
    public List<int> PropertyIds { get; set; } = new();

    public int UserId { get; set; }

    public bool IncludePriceAnalysis { get; set; } = true;

    public bool IncludeLocationAnalysis { get; set; } = true;

    public bool IncludeFeatureAnalysis { get; set; } = true;

    public string Notes { get; set; } = string.Empty;
}
