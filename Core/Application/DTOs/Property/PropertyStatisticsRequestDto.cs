namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyStatisticsRequestDto
{
    public int? PropertyId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IncludeViews { get; set; } = true;

    public bool IncludeFavorites { get; set; } = true;

    public bool IncludeOffers { get; set; } = true;

    public bool IncludeRevenue { get; set; } = true;
}
