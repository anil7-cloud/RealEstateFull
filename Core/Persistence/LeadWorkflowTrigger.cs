namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadWorkflowTrigger
{
    public int Id { get; set; }

    public int LeadWorkflowId { get; set; }

    public string TriggerType { get; set; } = string.Empty;

    public string Condition { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
