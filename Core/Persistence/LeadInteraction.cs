namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadInteraction
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    public string InteractionType { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime InteractionDate { get; set; } = DateTime.UtcNow;

    public int DurationMinutes { get; set; }

    public string Result { get; set; } = string.Empty;

    public string NextAction { get; set; } = string.Empty;

    public DateTime? NextActionDate { get; set; }

    public bool RequiresFollowUp { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
