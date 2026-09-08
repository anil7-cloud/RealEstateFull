namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyVerificationRequestDto
{
    public int PropertyId { get; set; }

    public bool IsVerified { get; set; }

    public string VerificationStatus { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public int VerifiedByUserId { get; set; }

    public DateTime VerifiedAt { get; set; } = DateTime.UtcNow;
}
