namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadWorkflowDto
{
    public int UserId { get; set; }

    public string WorkflowType { get; set; } = string.Empty;

    public string CurrentStage { get; set; } = string.Empty;

    public string NextAction { get; set; } = string.Empty;

    public decimal SuccessProbability { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
