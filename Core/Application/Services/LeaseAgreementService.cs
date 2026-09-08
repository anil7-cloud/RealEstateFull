using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeaseAgreementService
{
    private readonly AppDbContext _context;

    public LeaseAgreementService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeaseAgreement> CreateAgreement(
        int propertyId,
        int landlordId,
        int tenantId,
        DateTime startDate,
        DateTime endDate,
        decimal monthlyRent)
    {
        var agreement = new LeaseAgreement
        {
            PropertyId = propertyId,
            LandlordId = landlordId,
            TenantId = tenantId,
            StartDate = startDate,
            EndDate = endDate,
            MonthlyRent = monthlyRent,
            Status = "Active",
            CreatedAt = DateTime.UtcNow
        };

        _context.LeaseAgreements.Add(agreement);

        await _context.SaveChangesAsync();

        return agreement;
    }


    public async Task<LeaseAgreement?> GetAgreementById(int id)
    {
        return await _context.LeaseAgreements
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<List<LeaseAgreement>> GetAllAgreements()
    {
        return await _context.LeaseAgreements
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<LeaseAgreement>> GetUserAgreements(int userId)
    {
        return await _context.LeaseAgreements
            .Where(x => x.LandlordId == userId || x.TenantId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> UpdateAgreementStatus(
        int id,
        string status)
    {
        var agreement = await _context.LeaseAgreements
            .FirstOrDefaultAsync(x => x.Id == id);

        if (agreement is null)
            return false;

        agreement.Status = status.Trim();

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAgreement(int id)
    {
        var agreement = await _context.LeaseAgreements
            .FirstOrDefaultAsync(x => x.Id == id);

        if (agreement is null)
            return false;

        _context.LeaseAgreements.Remove(agreement);

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<List<LeaseAgreement>> GetActiveAgreements()
    {
        return await _context.LeaseAgreements
            .Where(x => x.Status == "Active")
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<LeaseAgreement>> GetExpiredAgreements()
    {
        return await _context.LeaseAgreements
            .Where(x =>
                x.EndDate < DateTime.UtcNow &&
                x.Status == "Active")
            .OrderByDescending(x => x.EndDate)
            .ToListAsync();
    }

}
