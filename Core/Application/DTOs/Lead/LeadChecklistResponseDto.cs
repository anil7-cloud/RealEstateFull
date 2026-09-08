namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadChecklistResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string Title { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public int Order { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
