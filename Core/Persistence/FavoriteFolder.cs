namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class FavoriteFolder
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
