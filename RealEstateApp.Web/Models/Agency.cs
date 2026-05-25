namespace RealEstateApp.Web.Models;

public class Agency
{
    public int Id { get; set; }

    // Identity User ID

    public string? IdentityUserId { get; set; }

    // BASIC

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    // CONTACT

    public string Phone { get; set; } = string.Empty;

    public string WhatsAppNumber { get; set; } = string.Empty;

    // LOCATION

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    // DESCRIPTION

    public string Description { get; set; } = string.Empty;

    // RELATIONS

    public List<Listing> Listings { get; set; } = new();
}