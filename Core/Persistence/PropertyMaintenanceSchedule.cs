namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyMaintenanceSchedule
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string MaintenanceType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ScheduledDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public decimal EstimatedCost { get; set; }

    public decimal? ActualCost { get; set; }

    public string Status { get; set; } = "Planned";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
