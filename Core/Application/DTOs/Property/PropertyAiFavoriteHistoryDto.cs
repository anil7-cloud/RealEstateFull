namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiFavoriteHistoryDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public string ActionType { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
