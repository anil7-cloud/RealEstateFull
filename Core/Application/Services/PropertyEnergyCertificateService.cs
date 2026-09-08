using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyEnergyCertificateService
{
    private readonly AppDbContext _context;

    public PropertyEnergyCertificateService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyEnergyCertificate> CreateCertificate(
        PropertyEnergyCertificate certificate)
    {
        _context.PropertyEnergyCertificates.Add(certificate);

        await _context.SaveChangesAsync();

        return certificate;
    }

    public async Task<PropertyEnergyCertificate?> GetCertificate(
        int propertyId)
    {
        return await _context.PropertyEnergyCertificates
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.IssueDate)
            .FirstOrDefaultAsync();
    }

    public async Task<List<PropertyEnergyCertificate>> GetExpiredCertificates()
    {
        return await _context.PropertyEnergyCertificates
            .Where(x =>
                x.ExpirationDate < DateTime.UtcNow)
            .OrderBy(x => x.ExpirationDate)
            .ToListAsync();
    }

    public async Task<bool> RenewCertificate(
        int id,
        DateTime newExpirationDate)
    {
        var certificate = await _context.PropertyEnergyCertificates
            .FirstOrDefaultAsync(x => x.Id == id);

        if (certificate is null)
            return false;

        certificate.ExpirationDate = newExpirationDate;
        certificate.IsValid = true;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteCertificate(int id)
    {
        var certificate = await _context.PropertyEnergyCertificates
            .FirstOrDefaultAsync(x => x.Id == id);

        if (certificate is null)
            return false;

        _context.PropertyEnergyCertificates.Remove(certificate);

        await _context.SaveChangesAsync();

        return true;
    }
}
