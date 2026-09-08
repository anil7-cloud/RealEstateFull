namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyReminder
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime ReminderDate { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyListing? PropertyListing { get; set; }
}
