namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class SearchHistory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Keyword { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
