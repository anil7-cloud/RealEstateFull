namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadTask
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? PropertyId { get; set; }

    public int? AssignedUserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // Call, Meeting, Visit, Offer, Email, WhatsApp, FollowUp
    public string TaskType { get; set; } = "FollowUp";

    // Low, Normal, High, Urgent
    public string Priority { get; set; } = "Normal";

    // Pending, InProgress, Completed, Cancelled
    public string Status { get; set; } = "Pending";

    public DateTime DueDate { get; set; }

    public DateTime? ReminderDate { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string CompletionNote { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
