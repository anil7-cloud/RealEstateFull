namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyApprovalRequestDto
{
    public int PropertyId { get; set; }

    public bool IsApproved { get; set; }

    public string ApprovalStatus { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public int ApprovedByUserId { get; set; }

    public DateTime ApprovedAt { get; set; } = DateTime.UtcNow;
}
