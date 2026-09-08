namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyTenant
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string NationalId { get; set; } = string.Empty;

    public string EmergencyContact { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime MoveInDate { get; set; }

    public DateTime? MoveOutDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
