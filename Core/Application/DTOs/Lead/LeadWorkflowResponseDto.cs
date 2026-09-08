namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadWorkflowResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string WorkflowName { get; set; } = string.Empty;

    public string TriggerType { get; set; } = string.Empty;

    public string ActionType { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}
