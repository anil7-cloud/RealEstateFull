namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadStatusHistory
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string PreviousStatus { get; set; } = string.Empty;

    public string CurrentStatus { get; set; } = string.Empty;

    public int? ChangedByUserId { get; set; }

    public string ChangeReason { get; set; } = string.Empty;

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;

    public bool IsAutomatic { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
