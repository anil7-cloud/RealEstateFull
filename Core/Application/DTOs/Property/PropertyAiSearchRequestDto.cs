namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiSearchRequestDto
{
    public int UserId { get; set; }

    public string Query { get; set; } = string.Empty;

    public string? Location { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public string? PropertyType { get; set; }

    public int ResultCount { get; set; } = 10;
}
