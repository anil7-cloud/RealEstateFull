using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class EnergyCertificateService
{
    private readonly AppDbContext _context;

    public EnergyCertificateService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<EnergyCertificate> CreateCertificate(
        int propertyId,
        string certificateNumber,
        string energyClass,
        DateTime issueDate,
        DateTime expiryDate,
        string documentUrl)
    {
        if (string.IsNullOrWhiteSpace(certificateNumber))
            throw new ArgumentException(
                "Sertifika numarası boş olamaz.",
                nameof(certificateNumber));

        if (expiryDate <= issueDate)
            throw new ArgumentException(
                "Geçerlilik tarihi düzenlenme tarihinden sonra olmalıdır.");

        var certificate = new EnergyCertificate
        {
            PropertyId = propertyId,
            CertificateNumber = certificateNumber.Trim(),
            EnergyClass = energyClass.Trim().ToUpperInvariant(),
            IssueDate = issueDate,
            ExpiryDate = expiryDate,
            DocumentUrl = documentUrl.Trim(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.EnergyCertificates.Add(certificate);
        await _context.SaveChangesAsync();

        return certificate;
    }

    public async Task<List<EnergyCertificate>> GetPropertyCertificates(
        int propertyId)
    {
        return await _context.EnergyCertificates
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.IssueDate)
            .ToListAsync();
    }

    public async Task<EnergyCertificate?> GetActiveCertificate(
        int propertyId)
    {
        return await _context.EnergyCertificates
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsActive &&
                x.ExpiryDate >= DateTime.UtcNow)
            .OrderByDescending(x => x.IssueDate)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteCertificate(int id)
    {
        var certificate = await _context.EnergyCertificates
            .FirstOrDefaultAsync(x => x.Id == id);

        if (certificate is null)
            return false;

        _context.EnergyCertificates.Remove(certificate);
        await _context.SaveChangesAsync();

        return true;
    }
}
