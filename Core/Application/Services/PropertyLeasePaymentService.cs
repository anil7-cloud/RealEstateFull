using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyLeasePaymentService
{
    private readonly AppDbContext _context;

    public PropertyLeasePaymentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyLeasePayment> CreatePayment(
        PropertyLeasePayment payment)
    {
        _context.PropertyLeasePayments.Add(payment);

        await _context.SaveChangesAsync();

        return payment;
    }

    public async Task<List<PropertyLeasePayment>> GetLeasePayments(
        int leaseId)
    {
        return await _context.PropertyLeasePayments
            .Where(x => x.LeaseId == leaseId)
            .OrderByDescending(x => x.DueDate)
            .ToListAsync();
    }

    public async Task<List<PropertyLeasePayment>> GetOverduePayments()
    {
        return await _context.PropertyLeasePayments
            .Where(x =>
                x.Status == "Pending" &&
                x.DueDate < DateTime.UtcNow)
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }

    public async Task<bool> MarkAsPaid(
        int id,
        string paymentMethod)
    {
        var payment = await _context.PropertyLeasePayments
            .FirstOrDefaultAsync(x => x.Id == id);

        if (payment is null)
            return false;

        payment.Status = "Paid";
        payment.PaymentDate = DateTime.UtcNow;
        payment.PaymentMethod = paymentMethod.Trim();

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePayment(int id)
    {
        var payment = await _context.PropertyLeasePayments
            .FirstOrDefaultAsync(x => x.Id == id);

        if (payment is null)
            return false;

        _context.PropertyLeasePayments.Remove(payment);

        await _context.SaveChangesAsync();

        return true;
    }
}
