namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSegmentationDto
{
    public int UserId { get; set; }

    public string SegmentType { get; set; } = string.Empty;

    public string SegmentDescription { get; set; } = string.Empty;

    public decimal SegmentScore { get; set; }

    public string Recommendation { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
