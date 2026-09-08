namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadRatingDto
{
    public int LeadId { get; set; }

    public int Score { get; set; }

    public string Comment { get; set; } = string.Empty;

    public int? UserId { get; set; }

    public DateTime RatedAt { get; set; } = DateTime.UtcNow;
}
