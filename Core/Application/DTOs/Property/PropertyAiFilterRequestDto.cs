namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiFilterRequestDto
{
    public int UserId { get; set; }

    public string Query { get; set; } = string.Empty;

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public string? PropertyType { get; set; }

    public int Limit { get; set; } = 20;
}
