namespace RealEstateApp.Web.Models;

public class Appointment
{
    public int Id { get; set; }

    public int AgencyId { get; set; }
    public int? ListingId { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public DateTime Date { get; set; }
    public string Time { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}