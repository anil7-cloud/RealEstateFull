namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyFeatureRequestDto
{
    public int PropertyId { get; set; }

    public string FeatureName { get; set; } = string.Empty;

    public string FeatureValue { get; set; } = string.Empty;
}
