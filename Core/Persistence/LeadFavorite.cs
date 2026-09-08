namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadFavorite
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int PropertyId { get; set; }

    public int? UserId { get; set; }

    public DateTime FavoritedAt { get; set; } = DateTime.UtcNow;

    public int Priority { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsViewed { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
