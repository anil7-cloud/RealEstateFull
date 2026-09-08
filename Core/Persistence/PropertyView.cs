namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyView
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
