namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyVisit
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public Guid CustomerUserId { get; set; }

    public Guid AgentUserId { get; set; }

    public DateTime VisitDate { get; set; }

    public string Status { get; set; } = "Scheduled";

    public string? CustomerFeedback { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyListing? PropertyListing { get; set; }
}
