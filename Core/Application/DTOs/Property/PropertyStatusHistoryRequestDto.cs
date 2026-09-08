namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyStatusHistoryRequestDto
{
    public int PropertyId { get; set; }

    public int PreviousStatusId { get; set; }

    public int NewStatusId { get; set; }

    public string Reason { get; set; } = string.Empty;

    public int ChangedByUserId { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}
