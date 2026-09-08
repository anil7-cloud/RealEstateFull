namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadAssignTeamRequestDto
{
    public int LeadId { get; set; }

    public int TeamId { get; set; }

    public int AssignedUserId { get; set; }

    public string Note { get; set; } = string.Empty;
}
