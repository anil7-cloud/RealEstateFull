namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyReservation
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public decimal ReservationFee { get; set; }

    public DateTime ReservationDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public string Status { get; set; } = "Active";

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
