namespace RealEstateApp.Web.Models;

public class Listing
{
    public int Id { get; set; }
    public int AgencyId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int RoomCount { get; set; }

    public List<ListingImage> Images { get; set; } = new();
    public int GrossM2 { get; set; }
    public string CoverImageUrl { get; set; } = string.Empty;
    public string Status { get; set; } = "Active";

    public Agency? Agency { get; set; }
}