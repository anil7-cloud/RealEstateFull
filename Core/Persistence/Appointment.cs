namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class Appointment
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string Note { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
