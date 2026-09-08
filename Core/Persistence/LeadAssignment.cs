namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadAssignment
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int AssignedUserId { get; set; }

    public int? AssignedByUserId { get; set; }

    // Manual, Automatic
    public string AssignmentType { get; set; } = "Manual";

    // Active, Completed, Cancelled
    public string Status { get; set; } = "Active";

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    public string Reason { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsPrimary { get; set; } = true;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
