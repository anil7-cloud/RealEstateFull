namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyRestoreRequestDto
{
    public int PropertyId { get; set; }

    public bool Restore { get; set; }

    public string RestoreReason { get; set; } = string.Empty;

    public int RestoredByUserId { get; set; }

    public DateTime RestoredAt { get; set; } = DateTime.UtcNow;
}
