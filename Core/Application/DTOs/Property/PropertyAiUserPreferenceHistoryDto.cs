namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiUserPreferenceHistoryDto
{
    public int UserId { get; set; }

    public string PreferredLocation { get; set; } = string.Empty;

    public string PreferredPropertyType { get; set; } = string.Empty;

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public List<string> Features { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
