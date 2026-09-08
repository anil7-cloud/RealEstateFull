namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyReport
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public string Reason { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsResolved { get; set; } = false;
}
