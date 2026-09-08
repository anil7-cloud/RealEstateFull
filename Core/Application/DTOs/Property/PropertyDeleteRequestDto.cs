namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyDeleteRequestDto
{
    public int PropertyId { get; set; }

    public string DeleteReason { get; set; } = string.Empty;

    public int DeletedByUserId { get; set; }

    public bool PermanentDelete { get; set; }

    public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
}
