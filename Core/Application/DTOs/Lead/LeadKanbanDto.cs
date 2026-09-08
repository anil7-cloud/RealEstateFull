namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadKanbanDto
{
    public string Status { get; set; } = string.Empty;

    public int Count { get; set; }

    public List<LeadSummaryDto> Leads { get; set; } = new();
}
