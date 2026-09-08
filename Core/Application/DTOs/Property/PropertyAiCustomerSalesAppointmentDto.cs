namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesAppointmentDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string AppointmentType { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
