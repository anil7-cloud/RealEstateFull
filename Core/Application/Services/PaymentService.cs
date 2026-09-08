using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PaymentService
{
    private readonly AppDbContext _context;

    public PaymentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Payment> CreatePayment(
        int userId,
        int propertyId,
        decimal amount,
        string paymentMethod)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Ödeme tutarı sıfırdan büyük olmalıdır.");
        }

        var payment = new Payment
        {
            UserId = userId,
            PropertyId = propertyId,
            Amount = amount,
            Currency = "TRY",
            PaymentMethod = paymentMethod.Trim(),
            TransactionId = Guid.NewGuid().ToString("N"),
            Status = "Pending",
            PaidAt = DateTime.UtcNow
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return payment;
    }

    public async Task<List<Payment>> GetAllPayments()
    {
        return await _context.Payments
            .OrderByDescending(x => x.PaidAt)
            .ToListAsync();
    }

    public async Task<Payment?> GetPaymentById(int id)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Payment>> GetUserPayments(int userId)
    {
        return await _context.Payments
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.PaidAt)
            .ToListAsync();
    }

    public async Task<List<Payment>> GetPendingPayments()
    {
        return await _context.Payments
            .Where(x => x.Status == "Pending")
            .OrderByDescending(x => x.PaidAt)
            .ToListAsync();
    }

    public async Task<bool> UpdatePaymentStatus(
        int paymentId,
        string status)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(x => x.Id == paymentId);

        if (payment is null)
            return false;

        payment.Status = status.Trim();

        if (string.Equals(
            status,
            "Paid",
            StringComparison.OrdinalIgnoreCase))
        {
            payment.PaidAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePayment(int paymentId)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(x => x.Id == paymentId);

        if (payment is null)
            return false;

        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<decimal> GetTotalRevenue()
    {
        return await _context.Payments
            .Where(x =>
                x.Status == "Paid" ||
                x.Status == "Completed")
            .SumAsync(x => x.Amount);
    }

    public async Task<int> GetPaymentCount()
    {
        return await _context.Payments.CountAsync();
    }
}
