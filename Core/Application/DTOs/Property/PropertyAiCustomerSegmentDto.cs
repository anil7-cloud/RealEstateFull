namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSegmentDto
{
    public int UserId { get; set; }

    public string SegmentType { get; set; } = string.Empty;

    public string SegmentDescription { get; set; } = string.Empty;

    public decimal SegmentScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
