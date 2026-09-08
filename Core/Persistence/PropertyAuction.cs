namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyAuction
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public decimal StartingPrice { get; set; }

    public decimal CurrentHighestBid { get; set; }

    public int? HighestBidUserId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
