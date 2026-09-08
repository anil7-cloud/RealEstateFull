namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiDemandResultDto
{
    public int PropertyId { get; set; }

    public decimal DemandScore { get; set; }

    public int ViewCount { get; set; }

    public int FavoriteCount { get; set; }

    public int InquiryCount { get; set; }

    public string DemandLevel { get; set; } = string.Empty;

    public string Analysis { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
