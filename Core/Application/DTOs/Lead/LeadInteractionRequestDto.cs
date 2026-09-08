namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadInteractionRequestDto
{
    public int LeadId { get; set; }

    public string InteractionType { get; set; } = "";

    public string Note { get; set; } = "";
}
