namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiRankingResultDto
{
    public int PropertyId { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal RankingScore { get; set; }

    public string RankingReason { get; set; } = string.Empty;

    public int Rank { get; set; }
}
