namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyShareRequestDto
{
    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public string Platform { get; set; } = string.Empty;

    public string ShareUrl { get; set; } = string.Empty;

    public DateTime SharedAt { get; set; } = DateTime.UtcNow;
}
