namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyActivityRequestDto
{
    public int PropertyId { get; set; }

    public string ActivityType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int? UserId { get; set; }

    public DateTime ActivityDate { get; set; } = DateTime.UtcNow;
}
