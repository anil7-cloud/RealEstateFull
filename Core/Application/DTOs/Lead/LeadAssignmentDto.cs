namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadAssignmentDto
{
    public int LeadId { get; set; }

    public int UserId { get; set; }

    public string AssignmentType { get; set; } = "Manual";

    public string Notes { get; set; } = string.Empty;
}
