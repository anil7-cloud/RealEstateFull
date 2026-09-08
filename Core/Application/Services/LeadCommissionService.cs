using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadCommissionService
{
    private readonly AppDbContext _context;

    public LeadCommissionService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadCommission> CreateAsync(
        int leadId,
        int propertyId,
        decimal salePrice,
        decimal commissionRate)
    {
        var commissionAmount = salePrice * commissionRate / 100;

        var commission = new LeadCommission
        {
            LeadId = leadId,
            PropertyId = propertyId,
            SalePrice = salePrice,
            CommissionRate = commissionRate,
            CommissionAmount = commissionAmount,
            PaidAmount = 0,
            RemainingAmount = commissionAmount,
            Status = "Pending",
            Notes = string.Empty,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadCommissions.Add(commission);

        await _context.SaveChangesAsync();

        return commission;
    }


    public async Task<List<LeadCommission>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadCommissions
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> PayAsync(
        int id,
        decimal amount)
    {
        var commission = await _context.LeadCommissions
            .FirstOrDefaultAsync(x => x.Id == id);

        if (commission == null)
            return false;

        commission.PaidAmount += amount;

        commission.RemainingAmount =
            commission.CommissionAmount - commission.PaidAmount;

        commission.Status =
            commission.RemainingAmount <= 0
            ? "Paid"
            : "Partial";

        commission.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
