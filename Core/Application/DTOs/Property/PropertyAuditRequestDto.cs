namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAuditRequestDto
{
    public int PropertyId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int? UserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
