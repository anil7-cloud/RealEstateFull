namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadAssignTeamDto
{
    public int LeadId { get; set; }

    public int TeamId { get; set; }

    public int AssignedUserId { get; set; }

    public string Note { get; set; } = string.Empty;

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}
