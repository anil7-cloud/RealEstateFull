namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadAlert
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string AlertName { get; set; } = string.Empty;

    public string PropertyType { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int? MinRooms { get; set; }

    public int? MaxRooms { get; set; }

    public bool EmailNotification { get; set; }

    public bool SmsNotification { get; set; }

    public bool PushNotification { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTime? LastTriggeredAt { get; set; }

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
