namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiViewHistoryDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public DateTime ViewDate { get; set; } = DateTime.UtcNow;

    public string Source { get; set; } = string.Empty;

    public string DeviceType { get; set; } = string.Empty;
}
