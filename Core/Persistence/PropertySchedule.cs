namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertySchedule
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public string ScheduleType { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsActive { get; set; } = true;

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyListing? PropertyListing { get; set; }
}
