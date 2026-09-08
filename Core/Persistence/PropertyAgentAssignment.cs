namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyAgentAssignment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public Guid AgentUserId { get; set; }

    public string Role { get; set; } = "Agent";

    public bool IsPrimary { get; set; }

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RemovedAt { get; set; }

    public PropertyListing? PropertyListing { get; set; }
}
