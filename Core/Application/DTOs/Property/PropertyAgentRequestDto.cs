namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAgentRequestDto
{
    public int PropertyId { get; set; }

    public int AgentId { get; set; }

    public string Role { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}
