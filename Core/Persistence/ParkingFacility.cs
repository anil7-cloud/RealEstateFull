namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ParkingFacility
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ParkingType { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public decimal DistanceKm { get; set; }

    public decimal HourlyPrice { get; set; }

    public bool IsCovered { get; set; }

    public bool IsOpen24Hours { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
