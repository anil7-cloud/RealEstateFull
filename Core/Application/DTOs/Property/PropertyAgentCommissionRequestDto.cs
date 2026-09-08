namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAgentCommissionRequestDto
{
    public int PropertyId { get; set; }

    public int AgentId { get; set; }

    public decimal CommissionRate { get; set; }

    public decimal CommissionAmount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
