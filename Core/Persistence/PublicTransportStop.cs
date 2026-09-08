
namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PublicTransportStop

{

    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string TransportType { get; set; } = string.Empty;

    public decimal DistanceKm { get; set; }

    public string LineNames { get; set; } = string.Empty;

    public bool IsAccessible { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}

