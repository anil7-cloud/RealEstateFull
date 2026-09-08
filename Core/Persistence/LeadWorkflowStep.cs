namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadWorkflowStep
{
    public int Id { get; set; }

    public int LeadWorkflowId { get; set; }

    public string StepName { get; set; } = string.Empty;

    public int StepOrder { get; set; }

    public string StepType { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public int? AssignedUserId { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? DueDate { get; set; }

    public bool IsRequired { get; set; }

    public bool IsCompleted { get; set; }

    public string Result { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
