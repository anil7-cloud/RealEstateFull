namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyKeyHandover
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int? DeliveredByUserId { get; set; }

    public int? ReceivedByUserId { get; set; }

    public string ReceiverName { get; set; } = string.Empty;

    public int KeyCount { get; set; }

    public DateTime HandoverDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public string Status { get; set; } = "Delivered";

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
