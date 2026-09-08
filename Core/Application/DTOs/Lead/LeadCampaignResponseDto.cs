namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadCampaignResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string CampaignName { get; set; } = string.Empty;

    public string CampaignType { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}
