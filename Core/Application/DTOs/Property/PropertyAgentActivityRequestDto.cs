namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAgentActivityRequestDto
{
    public int PropertyId { get; set; }

    public int AgentId { get; set; }

    public string ActivityType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ActivityDate { get; set; } = DateTime.UtcNow;
}
