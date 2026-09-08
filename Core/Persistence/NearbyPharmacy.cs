namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class NearbyPharmacy
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public decimal DistanceKm { get; set; }

    public bool IsOnDuty { get; set; }

    public string Phone { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
