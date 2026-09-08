namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerInteractionHistoryDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public string InteractionType { get; set; } = string.Empty;

    public string InteractionResult { get; set; } = string.Empty;

    public decimal EngagementScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
