namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyWorkflow
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public string WorkflowName { get; set; } = string.Empty;

    public string CurrentStep { get; set; } = string.Empty;

    public string Status { get; set; } = "Active";

    public string? WorkflowDataJson { get; set; }

    public Guid? AssignedUserId { get; set; }

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    public PropertyListing? PropertyListing { get; set; }
}
