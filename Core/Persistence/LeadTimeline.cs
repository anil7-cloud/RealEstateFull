namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadTimeline
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? PropertyId { get; set; }

    public int? UserId { get; set; }

    // Call, Email, Meeting, Note, SMS, Offer, Document...
    public string EventType { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Icon { get; set; } = string.Empty;

    public string Color { get; set; } = "#2196F3";

    public DateTime EventDate { get; set; } = DateTime.UtcNow;

    public bool IsVisible { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
