namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadWorkflowRequestDto
{
    public int LeadId { get; set; }

    public string WorkflowName { get; set; } = string.Empty;

    public string TriggerType { get; set; } = string.Empty;

    public string ActionType { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
