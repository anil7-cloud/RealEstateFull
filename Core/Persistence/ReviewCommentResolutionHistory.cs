namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistory
{
    public int Id { get; set; }

    public int ReviewCommentResolutionId { get; set; }

    public string PreviousResolution { get; set; } = string.Empty;

    public string CurrentResolution { get; set; } = string.Empty;

    public string ChangedBy { get; set; } = string.Empty;

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolution? ReviewCommentResolution { get; set; }
}
