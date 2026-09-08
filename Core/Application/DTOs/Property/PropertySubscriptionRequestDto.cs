namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertySubscriptionRequestDto
{
    public int UserId { get; set; }

    public string SubscriptionType { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsActive { get; set; } = true;
}
