namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyViewRequestDto
{
    public int PropertyId { get; set; }

    public int? UserId { get; set; }

    public string IpAddress { get; set; } = string.Empty;

    public DateTime ViewedAt { get; set; } = DateTime.UtcNow;
}
