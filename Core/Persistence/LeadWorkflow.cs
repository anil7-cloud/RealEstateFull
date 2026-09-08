namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadWorkflow
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string WorkflowName { get; set; } = string.Empty;

    public string CurrentStep { get; set; } = string.Empty;

    public int StepOrder { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    public int AssignedUserId { get; set; }

    public string NextAction { get; set; } = string.Empty;

    public DateTime? NextActionDate { get; set; }

    public bool IsCompleted { get; set; }

    public bool IsPaused { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
