namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertySearchHistoryRequestDto
{
    public int UserId { get; set; }

    public string SearchText { get; set; } = string.Empty;

    public string? City { get; set; }

    public string? District { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
