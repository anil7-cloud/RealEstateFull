namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiDemandRequestDto
{
    public int PropertyId { get; set; }

    public string DemandType { get; set; } = string.Empty;

    public decimal DemandScore { get; set; }

    public int ViewCount { get; set; }

    public int FavoriteCount { get; set; }

    public int InquiryCount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
