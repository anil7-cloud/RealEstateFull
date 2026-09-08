namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyRankingRequestDto
{
    public int? PropertyId { get; set; }

    public decimal Score { get; set; }

    public string RankingType { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
