namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiUserInteractionHistoryDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public string InteractionType { get; set; } = string.Empty;

    public string Details { get; set; } = string.Empty;

    public DateTime InteractionDate { get; set; } = DateTime.UtcNow;
}
