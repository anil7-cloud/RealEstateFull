namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyHistoryRequestDto
{
    public int PropertyId { get; set; }

    public string ActionType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int? UserId { get; set; }

    public DateTime ActionDate { get; set; } = DateTime.UtcNow;
}
