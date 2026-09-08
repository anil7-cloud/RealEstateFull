namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class CompareItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PropertyId { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
