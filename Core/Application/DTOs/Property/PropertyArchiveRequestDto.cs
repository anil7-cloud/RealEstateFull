namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyArchiveRequestDto
{
    public int PropertyId { get; set; }

    public bool IsArchived { get; set; }

    public string ArchiveReason { get; set; } = string.Empty;

    public int ArchivedByUserId { get; set; }

    public DateTime ArchivedAt { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;
}
