namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class Neighborhood
{
    public int Id { get; set; }

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public decimal AverageSalePrice { get; set; }

    public decimal AverageRentPrice { get; set; }

    public int Population { get; set; }

    public double SafetyScore { get; set; }

    public double TransportationScore { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
