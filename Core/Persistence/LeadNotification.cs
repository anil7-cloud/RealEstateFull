
namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadNotification

{

    public int Id { get; set; }

    public int? LeadId { get; set; }

    public int? UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string Priority { get; set; } = "Normal";

    public string Icon { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public string ActionUrl { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public bool IsArchived { get; set; }

    public bool IsSent { get; set; }

    public DateTime? ReadAt { get; set; }

    public DateTime? SentAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

}

