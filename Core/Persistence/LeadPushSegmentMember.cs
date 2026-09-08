namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushSegmentMember
{
    public int Id { get; set; }

    public int LeadPushSegmentId { get; set; }

    public int UserId { get; set; }

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    public DateTime? RemovedAt { get; set; }

    public string Notes { get; set; } = string.Empty;

    public LeadPushSegment? Segment { get; set; }
}
