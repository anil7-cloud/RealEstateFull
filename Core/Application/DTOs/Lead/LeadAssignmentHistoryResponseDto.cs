namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadAssignmentHistoryResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int PreviousUserId { get; set; }

    public string? PreviousUserName { get; set; }

    public int NewUserId { get; set; }

    public string? NewUserName { get; set; }

    public string Reason { get; set; } = string.Empty;

    public DateTime AssignedAt { get; set; }
}
