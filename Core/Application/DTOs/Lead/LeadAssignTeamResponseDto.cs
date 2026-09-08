namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadAssignTeamResponseDto
{
    public int LeadId { get; set; }

    public int TeamId { get; set; }

    public int AssignedUserId { get; set; }

    public string Message { get; set; } = "Lead assigned to team successfully";

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}
