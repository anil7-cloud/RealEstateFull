namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyReviewRequestDto
{
    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; } = string.Empty;

    public bool IsApproved { get; set; }
}
