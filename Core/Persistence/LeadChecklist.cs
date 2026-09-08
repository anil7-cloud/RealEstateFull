namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadChecklist
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public DateTime? CompletedAt { get; set; }

    public int Priority { get; set; }

    public DateTime? DueDate { get; set; }

    public int? AssignedUserId { get; set; }

    public string Category { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
