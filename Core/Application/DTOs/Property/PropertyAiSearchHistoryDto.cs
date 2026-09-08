namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiSearchHistoryDto
{
    public int UserId { get; set; }

    public string Query { get; set; } = string.Empty;

    public string? Location { get; set; }

    public string? PropertyType { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int ResultCount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
