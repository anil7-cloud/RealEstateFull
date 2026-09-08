namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadAppointmentDto
{
    public int LeadId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }
}
