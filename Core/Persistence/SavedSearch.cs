namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class SavedSearch
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string SearchName { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int? MinRooms { get; set; }

    public int? MaxRooms { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string Name { get; set; } = string.Empty;
    public string Query { get; set; } = string.Empty;
}
