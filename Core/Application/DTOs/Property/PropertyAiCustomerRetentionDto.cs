namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerRetentionDto
{
    public int UserId { get; set; }

    public decimal RetentionScore { get; set; }

    public string RetentionLevel { get; set; } = string.Empty;

    public string RetentionReason { get; set; } = string.Empty;

    public List<string> Recommendations { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
