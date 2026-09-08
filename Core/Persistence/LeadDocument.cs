namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadDocument
{
    public int Id { get; set; }

    // Potansiyel müşteri
    public int LeadId { get; set; }

    // İlgili portföy
    public int? PropertyId { get; set; }

    // Yükleyen kullanıcı
    public int? UserId { get; set; }

    // Kimlik, Teklif, Sözleşme, Tapu vb.
    public string DocumentType { get; set; } = string.Empty;

    // Belge adı
    public string Title { get; set; } = string.Empty;

    // Dosya adı
    public string FileName { get; set; } = string.Empty;

    // Dosya yolu
    public string FilePath { get; set; } = string.Empty;

    // İçerik tipi
    public string ContentType { get; set; } = string.Empty;

    // Dosya boyutu (byte)
    public long FileSize { get; set; }

    // Son kullanma tarihi (opsiyonel)
    public DateTime? ExpirationDate { get; set; }

    // Aktif mi?
    public bool IsActive { get; set; } = true;

    // Not
    public string Notes { get; set; } = string.Empty;

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
