using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPaymentService
{
    private readonly AppDbContext _context;

    public LeadPaymentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadPayment?> CreateAsync(
        LeadPayment payment,
        int userId)
    {
        if (userId <= 0 || payment.LeadId <= 0)
            return null;

        payment.UserId = userId;
        payment.CreatedAt = DateTime.UtcNow;
        payment.IsActive = true;

        _context.LeadPayments.Add(payment);
        await _context.SaveChangesAsync();

        return payment;
    }

    public async Task<List<LeadPayment>> GetByLeadAsync(
        int leadId,
        int userId)
    {
        if (leadId <= 0 || userId <= 0)
            return new List<LeadPayment>();

        return await _context.LeadPayments
            .Where(x =>
                x.LeadId == leadId &&
                x.UserId == userId &&
                x.IsActive)
            .OrderByDescending(x => x.PaymentDate)
            .ToListAsync();
    }

    public async Task<LeadPayment?> GetByIdAsync(
        int id,
        int userId)
    {
        if (id <= 0 || userId <= 0)
            return null;

        return await _context.LeadPayments
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.UserId == userId &&
                x.IsActive);
    }

    public async Task<bool> UpdateAsync(
        LeadPayment updated,
        int userId)
    {
        if (updated.Id <= 0 || userId <= 0)
            return false;

        var payment = await _context.LeadPayments
            .FirstOrDefaultAsync(x =>
                x.Id == updated.Id &&
                x.UserId == userId &&
                x.IsActive);

        if (payment is null)
            return false;

        payment.Amount = updated.Amount;
        payment.Currency = updated.Currency;
        payment.PaymentMethod = updated.PaymentMethod;
        payment.PaymentDate = updated.PaymentDate;
        payment.Status = updated.Status;
        payment.TransactionNumber = updated.TransactionNumber;
        payment.InvoiceId = updated.InvoiceId;
        payment.Notes = updated.Notes;
        payment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(
        int id,
        int userId)
    {
        if (id <= 0 || userId <= 0)
            return false;

        var payment = await _context.LeadPayments
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.UserId == userId &&
                x.IsActive);

        if (payment is null)
            return false;

        payment.IsActive = false;
        payment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
