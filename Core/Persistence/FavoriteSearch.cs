namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class FavoriteSearch
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string SearchName { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int? MinRoomCount { get; set; }

    public int? MaxRoomCount { get; set; }

    public string PropertyType { get; set; } = string.Empty;

    public bool NotificationEnabled { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
