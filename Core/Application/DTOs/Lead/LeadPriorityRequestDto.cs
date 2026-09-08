namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadPriorityRequestDto
{
    public int LeadId { get; set; }

    public string Priority { get; set; } = "Normal";

    public string Reason { get; set; } = string.Empty;

    public int UpdatedByUserId { get; set; }
}
