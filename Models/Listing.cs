namespace REAL_ESTATE_CLEAN.Models
{
    public class Listing
    {
        public int Id { get; set; }
public int AgencyId { get; set; }
        public string Title { get; set; } = "";
        public string City { get; set; } = "";
        public decimal Price { get; set; }
    }
}
