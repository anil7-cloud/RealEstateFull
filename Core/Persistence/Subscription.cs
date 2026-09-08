namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class Subscription
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string PlanName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
