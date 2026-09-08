namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyPortfolio
{
    public int Id { get; set; }

    public int OwnerUserId { get; set; }

    public string PortfolioName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal TotalValue { get; set; }

    public int PropertyCount { get; set; }

    public bool IsPublic { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
