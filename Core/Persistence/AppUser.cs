namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class AppUser
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = "User";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public ICollection<Lead> Leads { get; set; } = new List<Lead>();

    public ICollection<Property> Properties { get; set; } = new List<Property>();
}
