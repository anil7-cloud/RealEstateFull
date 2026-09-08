namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerInteractionDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public string InteractionType { get; set; } = string.Empty;

    public string InteractionResult { get; set; } = string.Empty;

    public decimal InteractionScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
