namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadCall
{
    public int Id { get; set; }

    // Potansiyel müşteri
    public int LeadId { get; set; }

    // İlgili portföy
    public int? PropertyId { get; set; }

    // Aramayı yapan kullanıcı
    public int? UserId { get; set; }

    // Aranan telefon
    public string PhoneNumber { get; set; } = string.Empty;

    // Incoming, Outgoing
    public string CallType { get; set; } = "Outgoing";

    // Scheduled, Completed, Missed, Busy, Rejected, NoAnswer
    public string Status { get; set; } = "Scheduled";

    // Görüşme süresi (saniye)
    public int DurationSeconds { get; set; }

    // Görüşme özeti
    public string Summary { get; set; } = string.Empty;

    // Sonuç
    public string Result { get; set; } = string.Empty;

    // Geri arama tarihi
    public DateTime? CallbackDate { get; set; }

    // Ses kaydı (opsiyonel)
    public string RecordingPath { get; set; } = string.Empty;

    public DateTime CallDate { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
