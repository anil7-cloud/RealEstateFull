namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadFeedback
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? PropertyId { get; set; }

    public int? UserId { get; set; }

    public int Rating { get; set; }

    public string FeedbackType { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Comment { get; set; } = string.Empty;

    public bool WouldRecommend { get; set; }

    public bool RequiresAction { get; set; }

    public string ActionTaken { get; set; } = string.Empty;

    public DateTime FeedbackDate { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
