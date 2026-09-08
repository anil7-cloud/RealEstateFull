namespace REAL_ESTATE_CLEAN.Models;

public class ListingImage
{
    public int Id { get; set; }
    public string Url { get; set; } = "";

    public int ListingId { get; set; }
}
