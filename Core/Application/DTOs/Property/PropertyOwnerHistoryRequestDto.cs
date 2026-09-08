namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyOwnerHistoryRequestDto
{
    public int PropertyId { get; set; }

    public int PreviousOwnerId { get; set; }

    public int NewOwnerId { get; set; }

    public string ChangeReason { get; set; } = string.Empty;

    public DateTime ChangeDate { get; set; } = DateTime.UtcNow;
}
