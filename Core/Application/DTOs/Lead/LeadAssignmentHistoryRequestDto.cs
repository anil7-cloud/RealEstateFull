namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadAssignmentHistoryRequestDto
{
    public int LeadId { get; set; }

    public int PreviousUserId { get; set; }

    public int NewUserId { get; set; }

    public string Reason { get; set; } = string.Empty;
}
