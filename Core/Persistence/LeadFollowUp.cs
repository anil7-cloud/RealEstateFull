namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadFollowUp
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    public string FollowUpType { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ScheduledAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string Status { get; set; } = "Pending";

    public int Priority { get; set; }

    public bool ReminderSent { get; set; }

    public string Result { get; set; } = string.Empty;

    public string NextStep { get; set; } = string.Empty;

    public DateTime? NextFollowUpDate { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
