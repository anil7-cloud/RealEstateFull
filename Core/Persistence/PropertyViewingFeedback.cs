namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyViewingFeedback
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public int Rating { get; set; }

    public bool WouldRecommend { get; set; }

    public bool InterestedInBuying { get; set; }

    public string Comment { get; set; } = string.Empty;

    public DateTime ViewedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
