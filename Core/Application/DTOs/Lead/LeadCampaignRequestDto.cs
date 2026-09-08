namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadCampaignRequestDto
{
    public int LeadId { get; set; }

    public string CampaignName { get; set; } = string.Empty;

    public string CampaignType { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsActive { get; set; } = true;
}
