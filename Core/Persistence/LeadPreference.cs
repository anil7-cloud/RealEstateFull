namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPreference
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string PropertyType { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string Neighborhood { get; set; } = string.Empty;

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int? MinRoomCount { get; set; }

    public int? MaxRoomCount { get; set; }

    public int? MinSquareMeters { get; set; }

    public int? MaxSquareMeters { get; set; }

    public bool Furnished { get; set; }

    public bool HasParking { get; set; }

    public bool HasElevator { get; set; }

    public bool AllowsPets { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
