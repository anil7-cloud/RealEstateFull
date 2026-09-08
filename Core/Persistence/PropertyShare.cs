namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyShare
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public int UserId { get; set; }
    public string Platform { get; set; } = string.Empty;
    public DateTime SharedAt { get; set; } = DateTime.UtcNow;
}
