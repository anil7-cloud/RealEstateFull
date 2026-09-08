namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAssignmentRequestDto
{
    public int PropertyId { get; set; }

    public int AssignedUserId { get; set; }

    public int? PreviousUserId { get; set; }

    public string Reason { get; set; } = string.Empty;

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}
