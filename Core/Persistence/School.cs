namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class School
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public decimal DistanceKm { get; set; }

    public string Address { get; set; } = string.Empty;

    public double Rating { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
