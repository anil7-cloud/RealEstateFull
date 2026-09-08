namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadChecklistRequestDto
{
    public int LeadId { get; set; }

    public string Title { get; set; } = string.Empty;

    public int Order { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? CompletedAt { get; set; }
}
