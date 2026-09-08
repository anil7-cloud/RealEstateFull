namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadFollowUpDto
{
    public int LeadId { get; set; }

    public DateTime FollowUpDate { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }
}
