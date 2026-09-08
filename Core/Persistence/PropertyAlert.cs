namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyAlert
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public string AlertName { get; set; } = string.Empty;

    public string? City { get; set; }

    public string? District { get; set; }

    public string? PropertyType { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastTriggeredAt { get; set; }
}
