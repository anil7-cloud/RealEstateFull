namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadSearchHistory
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string SearchText { get; set; } = string.Empty;

    public string PropertyType { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int ResultsCount { get; set; }

    public DateTime SearchedAt { get; set; } = DateTime.UtcNow;

    public string FiltersJson { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
