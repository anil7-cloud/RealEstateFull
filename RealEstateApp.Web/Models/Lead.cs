namespace RealEstateApp.Web.Models;

public class Lead
{
    public int Id { get; set; }

    public int AgencyId { get; set; }
    public int? ListingId { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Message { get; set; }

    public string Status { get; set; } = "New";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Agency? Agency { get; set; }
    public Listing? Listing { get; set; }
}