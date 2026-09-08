namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyTeamRequestDto
{
    public int PropertyId { get; set; }

    public List<int> TeamMemberIds { get; set; } = new();

    public string TeamRole { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}
