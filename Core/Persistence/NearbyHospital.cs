namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class NearbyHospital
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string HospitalType { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public decimal DistanceKm { get; set; }

    public bool HasEmergencyService { get; set; }

    public double Rating { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
