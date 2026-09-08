namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiFilterResultDto
{
    public int PropertyId { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public decimal MatchScore { get; set; }

    public string FilterReason { get; set; } = string.Empty;
}
