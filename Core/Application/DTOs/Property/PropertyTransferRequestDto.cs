namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyTransferRequestDto
{
    public int PropertyId { get; set; }

    public int FromUserId { get; set; }

    public int ToUserId { get; set; }

    public string TransferReason { get; set; } = string.Empty;

    public DateTime TransferDate { get; set; } = DateTime.UtcNow;
}
