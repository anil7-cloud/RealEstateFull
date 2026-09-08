namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Statistics;

public class PropertyStatisticsDto
{
    public int PropertyId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int ViewCount { get; set; }

    public int FavoriteCount { get; set; }

    public int MessageCount { get; set; }

    public int TotalInteractions =>
        ViewCount + FavoriteCount + MessageCount;
}
