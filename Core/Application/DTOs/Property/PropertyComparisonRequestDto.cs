namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyComparisonRequestDto
{
    public List<int> PropertyIds { get; set; } = new();

    public bool IncludePrice { get; set; } = true;

    public bool IncludeFeatures { get; set; } = true;

    public bool IncludeLocation { get; set; } = true;

    public bool IncludeImages { get; set; } = true;
}
