namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadTag
{
    public int Id { get; set; }

    // İlgili müşteri
    public int LeadId { get; set; }

    // Etiket adı
    public string Name { get; set; } = string.Empty;

    // Etiket rengi (#FF0000 vb.)
    public string Color { get; set; } = "#2196F3";

    // Açıklama
    public string Description { get; set; } = string.Empty;

    // Aktif mi?
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
