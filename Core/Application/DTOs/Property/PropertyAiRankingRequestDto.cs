namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiRankingRequestDto
{
    public int UserId { get; set; }

    public List<int> PropertyIds { get; set; } = new();

    public string RankingCriteria { get; set; } = string.Empty;

    public bool IncludePriceScore { get; set; } = true;

    public bool IncludeLocationScore { get; set; } = true;

    public bool IncludeFeatureScore { get; set; } = true;
}
