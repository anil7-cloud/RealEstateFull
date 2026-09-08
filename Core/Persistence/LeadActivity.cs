namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadActivity
{
    public int Id { get; set; }

    // Potansiyel müşteri
    public int LeadId { get; set; }

    // İlgili portföy (opsiyonel)
    public int? PropertyId { get; set; }

    // Görüşmeyi yapan kullanıcı
    public int? UserId { get; set; }

    // Phone, Meeting, Email, WhatsApp, SMS, Visit, Note
    public string ActivityType { get; set; } = string.Empty;

    // Başlık
    public string Title { get; set; } = string.Empty;

    // Açıklama
    public string Description { get; set; } = string.Empty;

    // Süre (dakika)
    public int DurationMinutes { get; set; }

    // Sonuç
    // Successful, Pending, Failed
    public string Result { get; set; } = "Pending";

    // Sonraki takip tarihi
    public DateTime? NextFollowUpDate { get; set; }

    // Tamamlandı mı?
    public bool IsCompleted { get; set; }

    public DateTime ActivityDate { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
