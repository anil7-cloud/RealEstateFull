namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPipeline
{
    public int Id { get; set; }

    // Potansiyel müşteri
    public int LeadId { get; set; }

    // İlgili portföy (opsiyonel)
    public int? PropertyId { get; set; }

    // Satış aşaması
    // New, Contacted, Meeting, Offer, Negotiation, Won, Lost
    public string Stage { get; set; } = "New";

    // Satış olasılığı (%)
    public int Probability { get; set; }

    // Beklenen satış tutarı
    public decimal ExpectedAmount { get; set; }

    // Gerçekleşen satış tutarı
    public decimal? FinalAmount { get; set; }

    // Sonraki görüşme tarihi
    public DateTime? NextFollowUpDate { get; set; }

    // Son iletişim tarihi
    public DateTime? LastContactDate { get; set; }

    // Kazanıldı mı?
    public bool IsWon { get; set; }

    // Kaybedildi mi?
    public bool IsLost { get; set; }

    // Kaybedilme nedeni
    public string? LostReason { get; set; }

    // Sorumlu personel
    public int? AssignedUserId { get; set; }

    // Notlar
    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
