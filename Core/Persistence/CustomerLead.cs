namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class CustomerLead
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? AssignedUserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public string Status { get; set; } = "New";

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastContactDate { get; set; }
}
