namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyReservationRequestDto
{
    public int PropertyId { get; set; }

    public int? UserId { get; set; }

    public DateTime ReservationDate { get; set; }

    public decimal DepositAmount { get; set; }

    public string Status { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;
}
