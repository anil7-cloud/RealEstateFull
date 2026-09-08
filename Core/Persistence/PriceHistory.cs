namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PriceHistory
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}
