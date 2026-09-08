namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAnalyticsRequestDto
{
    public int? PropertyId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IncludeViews { get; set; } = true;

    public bool IncludeFavorites { get; set; } = true;

    public bool IncludeInquiries { get; set; } = true;
}
