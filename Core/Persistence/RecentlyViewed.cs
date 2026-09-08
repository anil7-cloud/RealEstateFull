namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class RecentlyViewed
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PropertyId { get; set; }
    public DateTime ViewedAt { get; set; } = DateTime.UtcNow;
}
