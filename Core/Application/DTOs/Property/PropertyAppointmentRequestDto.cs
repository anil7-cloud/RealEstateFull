namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAppointmentRequestDto
{
    public int PropertyId { get; set; }

    public int? UserId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public int DurationMinutes { get; set; }
}
