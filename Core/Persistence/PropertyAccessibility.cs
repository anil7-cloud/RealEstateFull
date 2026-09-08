namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyAccessibility
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public bool HasWheelchairRamp { get; set; }

    public bool HasAccessibleElevator { get; set; }

    public bool HasWideDoors { get; set; }

    public bool HasAccessibleBathroom { get; set; }

    public bool HasAccessibleParking { get; set; }

    public bool HasStepFreeEntrance { get; set; }

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }


    // Eski servis uyumluluğu
    public Guid PropertyId
    {
        get => PropertyListingId;
        set => PropertyListingId = value;
    }


    public PropertyListing? PropertyListing { get; set; }
}
