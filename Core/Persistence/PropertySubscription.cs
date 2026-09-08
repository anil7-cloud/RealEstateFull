namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertySubscription
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public bool NotifyPriceChanges { get; set; } = true;

    public bool NotifyStatusChanges { get; set; } = true;

    public bool NotifyNewPhotos { get; set; } = true;

    public bool NotifyOpenHouse { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
