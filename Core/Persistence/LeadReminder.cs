namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadReminder
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? PropertyId { get; set; }

    public int? UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // Call, Meeting, Email, SMS, Document vb.
    public string ReminderType { get; set; } = string.Empty;

    public DateTime ReminderDate { get; set; }

    public bool IsCompleted { get; set; } = false;

    public DateTime? CompletedAt { get; set; }

    public bool IsCancelled { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
