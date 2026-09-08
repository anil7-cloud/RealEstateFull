namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyRecommendationRequestDto
{
    public int UserId { get; set; }

    public List<int> PropertyIds { get; set; } = new();

    public string PreferenceType { get; set; } = string.Empty;

    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public string Notes { get; set; } = string.Empty;
}
