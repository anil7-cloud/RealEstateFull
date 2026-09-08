namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadForecastAppointmentHistoryDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public int CustomerId { get; set; }

    public string AppointmentType { get; set; } = "";

    public DateTime AppointmentDate { get; set; }

    public string Status { get; set; } = "";

    public string Notes { get; set; } = "";
}
