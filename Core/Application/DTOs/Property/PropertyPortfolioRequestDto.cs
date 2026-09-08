namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyPortfolioRequestDto
{
    public int UserId { get; set; }

    public List<int> PropertyIds { get; set; } = new();

    public string PortfolioName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
