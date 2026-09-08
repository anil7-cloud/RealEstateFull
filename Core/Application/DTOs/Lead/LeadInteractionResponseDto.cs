namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadInteractionResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string InteractionType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int? UserId { get; set; }

    public string? UserName { get; set; }

    public DateTime InteractionDate { get; set; }

    public DateTime CreatedAt { get; set; }
}
