namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadWorkflowExecution
{
    public int Id { get; set; }

    public int LeadWorkflowId { get; set; }

    public int LeadId { get; set; }

    public string Status { get; set; } = "Running";

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
